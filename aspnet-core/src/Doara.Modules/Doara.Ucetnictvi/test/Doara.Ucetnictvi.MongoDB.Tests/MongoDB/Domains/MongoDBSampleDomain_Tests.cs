using Doara.Ucetnictvi.Samples;
using Xunit;

namespace Doara.Ucetnictvi.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<UcetnictviMongoDbTestModule>
{

}
