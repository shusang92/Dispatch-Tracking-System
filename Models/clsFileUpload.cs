using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
//using SFolderKM = MachineCalibrationSystem.vpwtsheion;
//using SUploadKM = MachineCalibrationSystem.vpwtsheion1;
using System.Xml;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ImportExcel.Models
{
    public class clsFileUpload : Controller
    {

    }

    public static class clsImplementationEnum
    {
        public enum RoleType
        {
            [StringValue("PMG")]
            PMG,
            [StringValue("LOG")]
            LOG
        }

        public static string GetStringValue(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }
    }

    #region String Attribute Class
    /// <summary>
    /// This attribute is used to represent a string value
    /// for a value in an enum.
    /// </summary>
    public class StringValueAttribute : Attribute
    {
        #region Properties

        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string StringValue { get; protected set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }

        #endregion
    }

    #endregion

    public class ChunkMergeUtility
    {
        public string FileName { get; set; }
        public string TempFolder { get; set; }
        public int MaxFileSizeMB { get; set; }
        public List<String> FileParts { get; set; }
        public ChunkMergeUtility()
        {
            FileParts = new List<string>();
        }
    }

    public struct SortedFile
    {
        public int FileOrder { get; set; }
        public String FileName { get; set; }
    }
    public class MergeFileManager
    {
        private static MergeFileManager instance;
        private List<string> MergeFileList;

        private MergeFileManager()
        {
            try
            {
                MergeFileList = new List<string>();
            }
            catch { }
        }

        public static MergeFileManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new MergeFileManager();
                return instance;
            }
        }
        public void AddFile(string BaseFileName)
        {
            MergeFileList.Add(BaseFileName);
        }
        public bool InUse(string BaseFileName)
        {
            return MergeFileList.Contains(BaseFileName);
        }
        public bool RemoveFile(string BaseFileName)
        {
            return MergeFileList.Remove(BaseFileName);
        }
    }
}