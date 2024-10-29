using MediatR;

namespace LT.Integration.Test
{
    [CollectionDefinition(Constants.TEST_COLLECTION)]
    public class LTTestCollection 
        : ICollectionFixture<DatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
