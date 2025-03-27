using Doara.Sklady.MongoDB;
using Doara.Sklady.Samples;
using Xunit;

namespace Doara.Sklady.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleAppService_Tests : SampleAppService_Tests<SkladyMongoDbTestModule>
{

}
