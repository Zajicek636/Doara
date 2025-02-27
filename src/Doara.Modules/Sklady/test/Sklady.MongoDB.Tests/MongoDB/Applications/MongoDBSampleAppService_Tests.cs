using Sklady.MongoDB;
using Sklady.Samples;
using Xunit;

namespace Sklady.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleAppService_Tests : SampleAppService_Tests<SkladyMongoDbTestModule>
{

}
