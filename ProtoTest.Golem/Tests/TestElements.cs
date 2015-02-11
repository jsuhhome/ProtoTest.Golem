﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using Gallio.Framework;
using Gallio.Framework.Pattern;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.PageObjects;
using ProtoTest.Golem.Core;
using ProtoTest.Golem.WebDriver;

namespace ProtoTest.Golem.Tests
{
    class TestElements : WebDriverTestBase
    {
    [Test]
        public void TestElementAPI()
    {
        driver.Navigate().GoToUrl("http://www.google.com");
        var elements = new ElementCollection(driver.FindElements(By.Name("q")));
        elements.First().Click();
    }

    [Test]
    public void TestLinq()
    {
        driver.Navigate().GoToUrl("http://www.google.com");
        var elements = new ElementCollection(driver.FindElements(By.XPath("//*")));
        elements.First(x=>x.GetAttribute("name")=="q" && x.Displayed).Click();
    }

    [Test]
    public void testCount()
    {
        driver.Navigate().GoToUrl("http://www.google.com");
        var elements = new ElementCollection(driver.FindElements(By.Name("q")));
        driver.Navigate().GoToUrl("http://www.google.com");
        Assert.Count(1,elements.Where(x=>x.IsStale()));
    }


    [Test]
    public void TestBy()
    {
        var elements = new ElementCollection(By.Name("q"));
        driver.Navigate().GoToUrl("http://www.google.com");        
        elements.ForEach(x => x.Click());
        driver.Navigate().GoToUrl("http://www.google.com"); 
        elements.ForEach(x => x.SendKeys("test"));
        
    }



    }
}
