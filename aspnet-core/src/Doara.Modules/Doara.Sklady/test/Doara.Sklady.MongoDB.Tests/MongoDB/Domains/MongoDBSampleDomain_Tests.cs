using Doara.Sklady.Samples;
using Xunit;

namespace Doara.Sklady.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<SkladyMongoDbTestModule>
{

}
