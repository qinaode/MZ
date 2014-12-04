/**  智客知识管理平台。
* ZK_FileExtend.cs
*
* 功 能： N/A
* 类 名： ZK_FileExtend
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:45   N/A    初版
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
	/// 1
	/// </summary>
	[Serializable]
	public partial class ZK_FileExtend
	{
		public ZK_FileExtend()
		{}
		#region Model
		private int _fileextendid;
		private int? _fileid;
		private string _fileextendname;
		private string _fileextendvalue;
		/// <summary>
		/// 
		/// </summary>
		public int fileExtendID
		{
			set{ _fileextendid=value;}
			get{return _fileextendid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? fileID
		{
			set{ _fileid=value;}
			get{return _fileid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string fileExtendName
		{
			set{ _fileextendname=value;}
			get{return _fileextendname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string fileExtendValue
		{
			set{ _fileextendvalue=value;}
			get{return _fileextendvalue;}
		}
		#endregion Model

	}
}

