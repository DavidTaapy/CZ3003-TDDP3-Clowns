using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestSuiteSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    [Test]
    public void Register_EmptyString_DisplaysWarningText(){
        User user = new User("harry", 300, 2);
        user.setId("1234567890");

        Assert.AreEqual(user.getId() , "1234567890");
        
    }
}
