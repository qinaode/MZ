/**  智客知识管理平台。
* ZK_FileType.cs
*
* 功 能： N/A
* 类 名： ZK_FileType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:46   N/A    初版
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
	public partial class ZK_FileType
	{
		public ZK_FileType()
		{}
		#region Model
		private int _filetypeid;
		private string _filetypename;
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
		#endregion Model

	}
}

