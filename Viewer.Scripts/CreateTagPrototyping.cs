﻿
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CAS.CommServer.DA.Viewer.Scripts
{

  [TestClass]
  public class CreateTagPrototyping
  {

    [TestMethod]
    public void CreateTagTest()
    {
      string _version = "2.01.1061"; //random version for testing purpose only

      #region Copy to script
      string _rel = _version.Replace(".", "_"); // _version must be replaced by the correct version variable depending on the project.
      string _repositoryUrl = @"svn://svnserver.hq.cas.com.pl/VS/";
      string _solutionPath = @"/CommServer.DA.Viewer";
      string _trunkPath = $@"{_repositoryUrl}trunk{_solutionPath}/";
      string _tagPath = $@"{_repositoryUrl}tags{_solutionPath}.rel_{_rel}";
      #endregion

      Assert.AreEqual("svn://svnserver.hq.cas.com.pl/VS/trunk/CommServer.DA.Viewer/", _trunkPath);
      Assert.AreEqual("svn://svnserver.hq.cas.com.pl/VS/tags/CommServer.DA.Viewer.rel_2_01_1061", _tagPath);

    }
  }

}
