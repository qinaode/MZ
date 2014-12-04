/**  智客知识管理平台。
* View_OtherFileList.cs
*
* 功 能： N/A
* 类 名： View_OtherFileList
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/28 13:41:47   N/A    初版
*
* Copyright (c) 2012 BeiJing HaoLian Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：北京浩联教育科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace ZK.Model
{
	/// <summary>
	/// View_OtherFileList:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class View_OtherFileList
	{
		public View_OtherFileList()
		{}
		#region Model
		private long? _id;
		private int _fileid;
		private string _filename;
		private int _cateid;
		private string _catename;
		private int _filetypeid;
		private string _filetypename;
		private int _channelid;
		private string _channelname;
		private DateTime? _createtime;
		private string _imageurl;
		private int? _clicknum;
		private int? _isdir;
		private string _filepath;
		private string _filedesc;
		private int _userid;
		private string _username;
		/// <summary>
		/// 
		/// </summary>
		public long? id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int fileID
		{
			set{ _fileid=value;}
			get{return _fileid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string fileName
		{
			set{ _filename=value;}
			get{return _filename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int cateID
		{
			set{ _cateid=value;}
			get{return _cateid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cateName
		{
			set{ _catename=value;}
			get{return _catename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int fileTypeID
		{
			set{ _filetypeid=value;}
			get{return _filetypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string fileTypeName
		{
			set{ _filetypename=value;}
			get{return _filetypename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int channelID
		{
			set{ _channelid=value;}
			get{return _channelid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string channelName
		{
			set{ _channelname=value;}
			get{return _channelname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? createTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string imageURL
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? clickNum
		{
			set{ _clicknum=value;}
			get{return _clicknum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? isDir
		{
			set{ _isdir=value;}
			get{return _isdir;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string filePath
		{
			set{ _filepath=value;}
			get{return _filepath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string fileDesc
		{
			set{ _filedesc=value;}
			get{return _filedesc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int USERID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string USERNAME
		{
			set{ _username=value;}
			get{return _username;}
		}
		#endregion Model

	}
}

