﻿using System;
using System.Collections.Generic;
using Gallio.Common.Media;
using Gallio.Framework;
using OpenQA.Selenium;
using ProtoTest.Golem.WebDriver;

namespace ProtoTest.Golem.Core
{
    public class TestDataContainer
    {
        private delegate void ActionEvent(string name, EventArgs e);


#pragma warning disable 67
        private static event ActionEvent BeforeTestEvent;
        private static event ActionEvent AfterTestEvent;
        private static event ActionEvent PageObjectActionEvent;
        private static event ActionEvent BeforeCommandEvent;
        private static event ActionEvent AfterCommandEvent;
        private static event ActionEvent BeforeSuiteEvent;
        private static event ActionEvent AfterSuiteEvent;
        private static event ActionEvent GenericEvent;
#pragma warning restore 67

        public List<VerificationError> VerificationErrors;
        public ActionList actions;
        public Element lastElement;
        public ScreenRecorder recorder;
        public string testName;
        public BrowserInfo browserInfo;
        public ConfigSettings configSettings;

        public TestDataContainer(string name)
        {
            testName = name;
            actions = new ActionList();
            VerificationErrors = new List<VerificationError>();
            SetupEvents();
            configSettings = new ConfigSettings();
        }


        public void LogEvent(string name)
        {
            WriteActionToLog(name,null);
        }


        private void WriteActionToLog(string name, EventArgs e)
        {
            TestBase.overlay.Text = name;
            if (Config.Settings.reportSettings.diagnosticLog)
                DiagnosticLog.WriteLine(string.Format("({0}) : {1}", DateTime.Now.ToString("HH:mm:ss::ffff"), name));
            if (Config.Settings.reportSettings.testLog)
                TestLog.WriteLine(string.Format("({0}) : {1}", DateTime.Now.ToString("HH:mm:ss::ffff"), name));
        }

        private void AddAction(string name, EventArgs e)
        {
            TestBase.testData.actions.addAction(name);
        }

        private object eventLocker = new object();
        private bool eventsRegistered = false;
        private void SetupEvents()
        {
            lock (eventLocker)
            {
                    PageObjectActionEvent += AddAction;
                    BeforeTestEvent += WriteActionToLog;
                    AfterTestEvent += WriteActionToLog;
                    GenericEvent += WriteActionToLog;
                    eventsRegistered = true;
            }

        }

        private void RemoveEvents()
        {
            lock (eventLocker)
            {
                if (eventsRegistered)
                {
                    PageObjectActionEvent -= AddAction;
                    BeforeTestEvent -= WriteActionToLog;
                    AfterTestEvent -= WriteActionToLog;
                    GenericEvent -= WriteActionToLog;
                    eventsRegistered = false;
                }

            }

        }

        public IWebDriver driver { get; set; }
    }
}