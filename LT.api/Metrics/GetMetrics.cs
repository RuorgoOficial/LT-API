using System.Diagnostics.Metrics;
using System.Reflection;

namespace LT.api.Metrics
{
    public class GetMetrics
    {
        private readonly Counter<int> _getCounter;
        public GetMetrics(IMeterFactory meterFactory)
        {
            var meter = meterFactory.Create("Lt.api");
            _getCounter = meter.CreateCounter<int>("lt.get");
        }
        public void GetCount(string controllerName, MethodBase? method) {
            if (method == null)
            {
                return;
            }
            _getCounter.Add(1, new KeyValuePair<string, object?>(controllerName, method.Name));
        }

    }
}
