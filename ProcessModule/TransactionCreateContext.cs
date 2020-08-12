using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace ProcessModule
{
    internal class TransactionCreateContext
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(TransactionCreateContext));
        private readonly Stopwatch _totalProcessingTime;
        private readonly ConcurrentDictionary<string, Stopwatch> _stepProcessingTimes;
        
        private TransactionCreateContext(InitialData initialData,
            Stopwatch totalProcessingTime)
        {
            _initialData = initialData;
            _totalProcessingTime = totalProcessingTime;
            _stepProcessingTimes = new ConcurrentDictionary<string, Stopwatch>();
        }

        public static TransactionCreateContext CreateNewContext()
        {
            var totalProcessingTime = Stopwatch.StartNew();

            var initialData = new InitialData
            {
                TransactionType = TransactionType.Fixed,
                CreatedDate = DateTime.Now
            };

            return new TransactionCreateContext(initialData, totalProcessingTime)
            {
                NewTransactionId = default(Guid)
            };
        }

        /// <summary> unique identifier for this instance of the copy process </summary>
        public Guid InstanceId { get; private set; }

        private string _lastRunStep;

        /// <summary> The last step the copy process tried to run </summary>
        public string LastAttemptedStep
        {
            get
            {
                return _lastRunStep;
            }
            internal set
            {
                _lastRunStep = value;
                if (Log.IsDebugEnabled)
                {
                    Log.Debug(String.Format("last attempted step for {0}: {1}", InstanceId, value));
                }
                _stepProcessingTimes.TryAdd(value, Stopwatch.StartNew());
            }
        }

        private string _lastFinishedStep;

        /// <summary> the last step the copy process successfully ran </summary>
        public string LastSuccessfulStep
        {
            get
            {
                return _lastFinishedStep;
            }
            internal set
            {
                _lastFinishedStep = value;
                if (Log.IsDebugEnabled)
                {
                    Log.Debug(String.Format("last successful step for {0}: {1}", InstanceId, value));
                }
            }
        }

        /// <summary> How many steps the copy process has completed </summary>
        public int StepsCompleted = 0;

        private int _totalSteps;

        /// <summary> How many steps the copy process has in it </summary>
        public int TotalSteps
        {
            get { return _totalSteps; }
        }
        public async Task SetTotalSteps(int steps)
        {
            _totalSteps = steps;
            await PersistWorkingData();
        }

        internal double PercentComplete
        {
            get { return TotalSteps != 0 ? Math.Round(StepsCompleted * 100 / (double)TotalSteps, 2) : 0; }
        }

        /// <summary> Whether or not the copy process is trying to resume an existing copy context </summary>
        public bool AttemptingResume { get; set; }

        /// <summary>
        /// The name of the step the copy process is trying to resume from.
        /// unused if AttemptingResume is false.
        /// </summary>
        public string ResumeAt { get; set; }

        /// <summary> How many modules nested in the copy process currently is </summary>
        public int ModuleDepth = 0;

        /// <summary> Safeguard against parallel execution of steps </summary>
        internal int SimultaneousSteps = 0;

        public Guid NewTransactionId { get; internal set; }
        private readonly InitialData _initialData;
        private WorkingData _workingData;

        internal long GetTotalElapsedMilliseconds()
        {
            return _totalProcessingTime.ElapsedMilliseconds;
        }

        internal long FinishTimingAndGetStepElapsedMilliseconds(string stepName)
        {
            Stopwatch stopwatch;
            if (_stepProcessingTimes.TryRemove(stepName, out stopwatch))
            {
                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
            }
            return -1;
        }

        /// <summary> Updates the database with known progress data </summary>
        public Task PersistProgress()
        {
            // since this happens A Lot, we go to direct parameterized sql to maximize performance with a blind update
            return Task.CompletedTask;
            //return _auditDataAccess.UpdateProgressData(CurrentUser.AccountId, InstanceId, LastAttemptedStep,
            //    LastSuccessfulStep, StepsCompleted, TotalSteps, ModuleDepth, NewEventId, CurrentUser.UserLoginName);
        }

        internal Task PersistWorkingData()
        {
            // if performance here is too bad, consider custom serializers so we only ever store the guid mappings
            // without including the database records.
            // we specify type name handling in order to be able to correctly deserialize the AdditionalContexts when
            // resuming a stuck copy
            string workingData = JsonConvert.SerializeObject(_workingData,
                    new JsonSerializerSettings
                    {
                        TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                        TypeNameHandling = TypeNameHandling.Objects
                    });
            // update db with workingData
            return Task.CompletedTask;
        }

        private class InitialData
        {
            public TransactionType TransactionType;
            public DateTime CreatedDate;
        }

        private class WorkingData
        {
            public Guid TemplateTransactionId;
            public Guid SourceTransactionId;
            public int TemplateAccountId;
        }
    }
}
