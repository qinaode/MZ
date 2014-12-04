/**  智客知识管理平台。
* ZK_FileList.cs
*
* 功 能： N/A
* 类 名： ZK_FileList
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/31 13:58:01   N/A    初版
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
	/// ZK_FileList:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ZK_FileList
	{
		public ZK_FileList()
		{}
		#region Model
		private int _fileid;
		private string _filename;
		private string _filepath;
		private int? _parentid;
		private int? _isdir;
		private int? _ownerid;
		private int? _filetypeid;
		private int? _clicknum;
		private DateTime? _createtime;
		private string _imageurl;
		private string _filedesc;
		private int? _trastatus;
		private int? _istraf;
		private int? _fileoldid;
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
		public string filePath
		{
			set{ _filepath=value;}
			get{return _filepath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? parentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
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
		public int? ownerID
		{
			set{ _ownerid=value;}
			get{return _ownerid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? fileTypeID
		{
			set{ _filetypeid=value;}
			get{return _filetypeid;}
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
		public string fileDesc
		{
			set{ _filedesc=value;}
			get{return _filedesc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? trastatus
		{
			set{ _trastatus=value;}
			get{return _trastatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? isTraf
		{
			set{ _istraf=value;}
			get{return _istraf;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? fileOldID
		{
			set{ _fileoldid=value;}
			get{return _fileoldid;}
		}
		#endregion Model

	}
}

