/**  智客知识管理平台。
* ZK_ChannelGroup.cs
*
* 功 能： N/A
* 类 名： ZK_ChannelGroup
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:42   N/A    初版
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
	public partial class ZK_ChannelGroup
	{
		public ZK_ChannelGroup()
		{}
		#region Model
		private int _channelgroupid;
		private int? _channelid;
		private string _channelgroupname;
		private string _channelgroupdesc;
		private int? _channelgroupparent;
		private int? _channelgrouplevel;
		/// <summary>
		/// 
		/// </summary>
		public int channelGroupID
		{
			set{ _channelgroupid=value;}
			get{return _channelgroupid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? channelID
		{
			set{ _channelid=value;}
			get{return _channelid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string channelGroupName
		{
			set{ _channelgroupname=value;}
			get{return _channelgroupname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string channelGroupDesc
		{
			set{ _channelgroupdesc=value;}
			get{return _channelgroupdesc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? channelGroupParent
		{
			set{ _channelgroupparent=value;}
			get{return _channelgroupparent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? channelGroupLevel
		{
			set{ _channelgrouplevel=value;}
			get{return _channelgrouplevel;}
		}
		#endregion Model

	}
}

