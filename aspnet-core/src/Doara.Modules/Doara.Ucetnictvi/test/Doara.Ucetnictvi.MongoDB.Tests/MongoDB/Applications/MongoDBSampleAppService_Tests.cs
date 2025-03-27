using Doara.Ucetnictvi.MongoDB;
using Doara.Ucetnictvi.Samples;
using Xunit;

namespace Doara.Ucetnictvi.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleAppService_Tests : SampleAppService_Tests<UcetnictviMongoDbTestModule>
{

}
