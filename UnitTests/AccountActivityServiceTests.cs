using System;
using Homework;
using Homework.ThirdParty;
using NUnit.Framework;

namespace UnitTests;


public class AccountActivityServiceTest
{
    private AccountActivityService _AccountActivityService;

    [SetUp]
    public void Setup()
    {
        FakeIAccountRepository temp = new FakeIAccountRepository();
        for (int i = 0; i < 5; i++)
        {
            temp.Add(new Account(i));
        }
        _AccountActivityService = new AccountActivityService(temp);
    }


    [Test]
    public void GetActivityTest()
    {
        Assert.That(_AccountActivityService.GetActivity(1) == ActivityLevel.None);
    }

    [Test]
    public void GetActivityExceptionTest()
    {
        Assert.That(() => _AccountActivityService.GetActivity(9), Throws.TypeOf<AccountNotExistsException>());

    }

    [Test]
    public void GetAmmountForActivityTest()
    {
        Assert.That(_AccountActivityService.GetAmountForActivity(ActivityLevel.None) == 5);
    }

}

