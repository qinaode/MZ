/**  智客知识管理平台。
* ZK_FileAndTags.cs
*
* 功 能： N/A
* 类 名： ZK_FileAndTags
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:44   N/A    初版
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
	public partial class ZK_FileAndTags
	{
		public ZK_FileAndTags()
		{}
		#region Model
		private int? _fileid;
		private int? _tagid;
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
		public int? tagID
		{
			set{ _tagid=value;}
			get{return _tagid;}
		}
		#endregion Model

	}
}

