using System.Collections;
using System.Collections.Generic;

namespace FlightProject.FunctionalObjects.NewsProviders
{
    public class NewsGenerator : IEnumerable<string>
    {
        public List<INewsVisitor> providers;
        public List<IReportable> reportables;

        public NewsGenerator(List<INewsVisitor> providers, List<IReportable> reportables)
        {
            this.providers = providers;
            this.reportables = reportables;
        }

        public string GenerateNextNews(INewsVisitor provider, IReportable reportableObject)
        {
            reportableObject.accept(provider);
            return provider.Report();
        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach (var provider in providers)
            {
                foreach (var reportableObject in reportables)
                {
                    string news = GenerateNextNews(provider, reportableObject);
                    yield return news;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }



    }
}
