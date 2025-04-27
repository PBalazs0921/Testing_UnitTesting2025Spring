
using Moq;

namespace UnitTests;

public class AccountTests
{
    private Account _Account;
    private Mock<IAction> _mockaction;

    [SetUp]
    public void Setup()
    {
        _Account = new Account(1);
        _mockaction = new Mock<IAction>();
        _mockaction.Setup(x => x.Execute()).Returns(true);
    }


    [Test]
    public void RegisterTest()
    {
        _Account.Register();
        Assert.That(_Account.IsRegistered == true);

    }

    [Test]
    public void ActivateTest()
    {
        _Account.Activate();
        Assert.That(_Account.IsConfirmed == true);

    }

    [Test]
    public void TakeActionTest()
    {
        //ARRANGEASS
        _Account.Register();
        _Account.Activate();

        //_mockaction.Setup(x => x.Execute()).Returns(true);
        Assert.That(_Account.TakeAction(_mockaction.Object) == true);
    }

    [Test]
    public void AccountInactiveException()
    {

        Assert.That(() => _Account.TakeAction(_mockaction.Object), Throws.TypeOf<InactiveUserException>());
    }
}
