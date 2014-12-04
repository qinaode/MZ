/**  智客知识管理平台。
* ZK_Tags.cs
*
* 功 能： N/A
* 类 名： ZK_Tags
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:49   N/A    初版
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
	public partial class ZK_Tags
	{
		public ZK_Tags()
		{}
		#region Model
		private int _tagid;
		private string _tagname;
		private int? _ownerid;
		private int? _relevantnum;
		private int? _relevantnumdy;
		private int? _relevantnumjx;
		private int? _relevantnumxz;
		private DateTime? _createtime;
		/// <summary>
		/// 
		/// </summary>
		public int tagID
		{
			set{ _tagid=value;}
			get{return _tagid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string tagName
		{
			set{ _tagname=value;}
			get{return _tagname;}
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
		public int? relevantNum
		{
			set{ _relevantnum=value;}
			get{return _relevantnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? relevantNumdy
		{
			set{ _relevantnumdy=value;}
			get{return _relevantnumdy;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? relevantNumjx
		{
			set{ _relevantnumjx=value;}
			get{return _relevantnumjx;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? relevantNumxz
		{
			set{ _relevantnumxz=value;}
			get{return _relevantnumxz;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? createTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		#endregion Model

	}
}

