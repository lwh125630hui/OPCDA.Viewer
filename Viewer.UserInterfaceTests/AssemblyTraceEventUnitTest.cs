﻿
using CAS.CommServer.DA.Viewer.Tests.Instrumentation;
using CAS.OPCViewer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CAS.CommServer.DA.Viewer.Tests
{
  [TestClass]
  public class AssemblyTraceEventUnitTest
  {

    [TestMethod]
    public void CreatorTest()
    {
      TraceSource _tracer = AssemblyTraceEvent.Tracer;
      Assert.IsNotNull(_tracer);
      Assert.AreEqual(2, _tracer.Listeners.Count);
      Dictionary<string, TraceListener> _listeners = _tracer.Listeners.Cast<TraceListener>().ToDictionary<TraceListener, string>(x => x.Name);
      Assert.IsTrue(_listeners.ContainsKey("LogFile"));
      TraceListener _listener = _listeners["LogFile"];
      Assert.IsNotNull(_listener);
      Assert.IsInstanceOfType(_listener, typeof(DelimitedListTraceListener));
      DelimitedListTraceListener _advancedListener = _listener as DelimitedListTraceListener;
      Assert.IsNotNull(_advancedListener.Filter);
      Assert.IsInstanceOfType(_advancedListener.Filter, typeof(EventTypeFilter));
      EventTypeFilter _eventTypeFilter = _advancedListener.Filter as EventTypeFilter;
      Assert.AreEqual(SourceLevels.All, _eventTypeFilter.EventType);
      string _testPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      Assert.AreEqual<string>(Path.Combine(_testPath, @"log\CAS.CommServer.DA.Viewer.log"), _advancedListener.GetFileName());
    }
    [TestMethod]
    public void LogFileExistsTest()
    {
      TraceSource _tracer = AssemblyTraceEvent.Tracer;
      TraceListener _listener = _tracer.Listeners.Cast<TraceListener>().Where<TraceListener>(x => x.Name == "LogFile").First<TraceListener>();
      Assert.IsNotNull(_listener);
      DelimitedListTraceListener _advancedListener = _listener as DelimitedListTraceListener;
      Assert.IsNotNull(_advancedListener);
      Assert.IsFalse(String.IsNullOrEmpty(_advancedListener.GetFileName()));
      FileInfo _logFileInfo = new FileInfo(_advancedListener.GetFileName());
      Assert.IsTrue(_logFileInfo.Exists);
      Assert.AreEqual<long>(0, _logFileInfo.Length);
      _tracer.TraceEvent(TraceEventType.Verbose, 0, "LogFileExistsTest is executed");
      Assert.IsFalse(String.IsNullOrEmpty(_advancedListener.GetFileName()));
      _logFileInfo.Refresh();
      Assert.IsTrue(_logFileInfo.Exists);
      Assert.IsTrue(_logFileInfo.Length > 10);
    }
  }
}
