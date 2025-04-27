using System.Security.Principal;
using Homework;
using Homework.ThirdParty;
using Moq;
using NUnit.Framework;

namespace Test;

[TestFixture]
public class AccountTests
{
    [Test]
    public void Register_ShouldSetIsRegisteredToTrue()
    {
        var account = new Account(1);
        account.Register();
        Assert.IsTrue(account.IsRegistered);
    }

    [Test]
    public void Activate_ShouldSetIsConfirmedToTrue()
    {
        var account = new Account(1);
        account.Activate();
        Assert.IsTrue(account.IsConfirmed);
    }

    [Test]
    public void TakeAction_ShouldThrow_WhenAccountIsNotRegistered()
    {
        var mockAction = new Mock<IAction>();
        var account = new Account(1);
        account.Activate(); // csak aktiválva, de nincs regisztrálva

        Assert.Throws<InactiveUserException>(() => account.TakeAction(mockAction.Object));
    }

    [Test]
    public void TakeAction_ShouldThrow_WhenAccountIsNotConfirmed()
    {
        var mockAction = new Mock<IAction>();
        var account = new Account(1);
        account.Register(); // csak regisztrálva, de nincs aktiválva

        Assert.Throws<InactiveUserException>(() => account.TakeAction(mockAction.Object));
    }

    [Test]
    public void TakeAction_ShouldIncreaseCounter_WhenActionSucceeds()
    {
        var mockAction = new Mock<IAction>();
        mockAction.Setup(a => a.Execute()).Returns(true);

        var account = new Account(1);
        account.Register();
        account.Activate();

        bool result = account.TakeAction(mockAction.Object);

        Assert.IsTrue(result);
        Assert.AreEqual(1, account.ActionsSuccessfullyPerformed);
    }

    [Test]
    public void TakeAction_ShouldNotIncreaseCounter_WhenActionFails()
    {
        var mockAction = new Mock<IAction>();
        mockAction.Setup(a => a.Execute()).Returns(false);

        var account = new Account(1);
        account.Register();
        account.Activate();

        bool result = account.TakeAction(mockAction.Object);

        Assert.IsFalse(result);
        Assert.AreEqual(0, account.ActionsSuccessfullyPerformed);
    }
}
