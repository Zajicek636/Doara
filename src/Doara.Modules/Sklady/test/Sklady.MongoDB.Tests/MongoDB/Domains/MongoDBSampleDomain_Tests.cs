using Sklady.Samples;
using Xunit;

namespace Sklady.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<SkladyMongoDbTestModule>
{

}
