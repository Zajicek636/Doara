using Doara.Sklady.Samples;
using Xunit;

namespace Doara.Sklady.MongoDB.Samples;

[Collection(MongoTestCollection.Name)]
public class SampleRepository_Tests : SampleRepository_Tests<SkladyMongoDbTestModule>
{
    /* Don't write custom repository tests here, instead write to
     * the base class.
     * One exception can be some specific tests related to MongoDB.
     */
}
