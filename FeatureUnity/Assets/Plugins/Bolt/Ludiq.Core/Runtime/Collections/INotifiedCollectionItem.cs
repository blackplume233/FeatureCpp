namespace Ludiq
{
    public interface INotifiedCollectionItem
    {
        void BeforeAdd();

        void AfterAdd();

        void BeforeRemove();

        void AfterRemove();
    }
}
