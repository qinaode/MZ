<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bojc_test.aspx.cs" Inherits="ZK.Manage.Bojc_test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>NBC知识管理后台 </title>
<link href="/nbc_schoolyard/PUBLIC/css/index.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="/nbc_schoolyard/PUBLIC/css/ext-all.css" />
<link rel="stylesheet" type="text/css" href="/nbc_schoolyard/PUBLIC/css/xtheme-gray.css" />

<script type="text/javascript" src="/nbc_schoolyard/PUBLIC/js/ext-base.js"></script>
<script type="text/javascript" src="/nbc_schoolyard/PUBLIC/js/ext-all.js"></script>
<script type="text/javascript" src="/nbc_schoolyard/PUBLIC/js/EasyLanguage.js"></script>
<script type="text/javascript" src="/nbc_schoolyard/PUBLIC/js/lib.js"></script>
<script type="text/javascript" src="/nbc_schoolyard/PUBLIC/js/PEG.js"></script>
<script type="text/javascript" src="/nbc_schoolyard/PUBLIC/js/jquery.js"></script>
</head>
<body>

    <script type="text/javascript">
var g_level = 0;

var Record = Ext.data.Record.create([
	{name: 'id', mapping:"id"},
	{name: 'name', mapping:"name"},
	{name: 'desc', mapping:"desc"},
	{name: 'level', mapping:"level"}
]);

var reader = new Ext.data.JsonReader({
	id: 'id',
	root: 'data',
	successProperty: 'success'
}, Record);

function renderName(value, metadata, record, rowIndex, colIndex, store)
{
	//alert(record.get('level'));
	var level = record.get('level');
	if( level == 2 )
		return "----" + value;
	else
		return value;
}
myGridPanel = Ext.extend(ns.gui.PEG, {
	
	height:500,
	width:800,
	region:'center',
	columns: [
		{id: 'id', menuDisabled: true, header: "标识", width: 90, align: 'left', dataIndex: 'id',hidden:true},
		{id: 'name', menuDisabled: true, header: "名称", width: 200, align: 'left', dataIndex: 'name', renderer: renderName},
		{id: 'desc', menuDisabled: true, header: "描述", width: 200, align: 'left', dataIndex: 'desc',hidden:true},
		{id: 'level', menuDisabled: true, header: "level", width: 200, align: 'left', dataIndex: 'level',hidden:true}
	],

	onEdit: function(record)
    {
        var action;

      if (!record) {
                record = new this.store.Record({});
                action = 'add';
				if(g_level==1){
        			this.popupEditor = g_edit1;	
        	
				}else if(g_level==2){
					this.popupEditor = g_edit2;
				}
        } else {
                action = 'edit';
               
                if(record.get('level')==1){
                	this.popupEditor = g_edit1;	
                	
                }else if(record.get('level')==2){
                	this.popupEditor = g_edit2;
                }	
        }
        
        
        this.popupEditor.show(action, record);
    },
    
	
	initComponent: function () {
            this.tbar = [{
                text: "添加行政分类",
                iconCls: 'button-icon-add',
                handler: function () {
                	g_level = 1;
                    this.onEdit();
                },
                scope: this
            },{
                text: "添加行政子分类",
                iconCls: 'button-icon-add',
                handler: function () {
                	g_level = 2;
                    this.onEdit();
                },
                scope: this
            },{
                text: "删除",
                iconCls: '/nbc_schoolyard/PUBLIC/images/button-icon-delete',
                disabled: true,
                handler: function () {
                        this.onDelete();
                },
                scope: this
            }];
            
            //this.popupEditor = g_editPanel;
            
            
            
            myGridPanel.superclass.initComponent.call(this);
    
	},
	
	initEvents: function () {
		myGridPanel.superclass.initEvents.call(this);
                var deleteBtn = this.getTopToolbar().items.items[2];
                this.getSelectionModel().on('selectionchange', function (sm) {
                        if (sm.getCount())
                                deleteBtn.enable();
                        else
                                deleteBtn.disable();
                });
	}
});

editPanel = Ext.extend(ns.gui.PEG.E, {
	baseTitle: "添加和编辑单元窗口",
	width: 550,
	frame: true,
	ethPairs:null,
	otherComponentId: [],
	initflag: true,
	selfName: '',
	
	saveCallback: function (option, success, response) {
		
		var result = Ext.decode(response.responseText);
		if(result.msg == false && result.msg.length>1)  //添加或者编辑的时候，报错了。
		{
			Ext.Msg.alert("确认", result.msg);
		}
		g_subjectPanel.store.reload();
		editPanel.superclass.saveCallback.call(this, option, success, response);
	},
	
	onRender: function (ct, position) {
		editPanel.superclass.onRender.call(this, ct, position);
	
		this.on('hide', function () {
			this.record = null;
			this.initflag = true;
		}, this);
		
		this.add({
			layout: 'form',
			labelWidth: 80,	
			baseCls: 'x-plain',
			
			items: [{
				name: 'name',
				xtype: 'textfield',
				maxLength: 30,
				allowBlank: false,
				fieldLabel: this.selfName+"名称",
				anchor: '98%',
				vtype: 'entityname'
			},{
				name: 'desc',
				xtype: 'textfield',
				maxLength: 300,
				allowBlank: false,
				fieldLabel: this.selfName+"描述",
				anchor: '98%',
				vtype: 'entityname'
			},{
				name: 'id',
				xtype: 'textfield',
				maxLength: 300,
				allowBlank: true,
				fieldLabel: "标示",
				anchor: '98%',
				vtype: 'entityname',
				hidden:true
				
			},{
				name: 'level',
				xtype: 'textfield',
				maxLength: 300,
				allowBlank: true,
				fieldLabel: "level",
				anchor: '98%',
				vtype: 'entityname',
				hidden:true
				
			}
			]
		});
	},
	show: function (action, record){	
		editPanel.superclass.show.call(this, action, record);
	}
});
var aVersion = [["1499","dfgdfg"],["1498","11"],];
var storeVersion = new Ext.data.SimpleStore({
	fields: ['id', 'name'],
	data : aVersion
});
var comboSubject = new Ext.form.ComboBox({
	name: 'parent_id',
	store: storeVersion,
	listWidth:150,
	valueField: 'id',
	displayField:'name',
	fieldLabel: "科目",
	typeAhead: true,
	mode: 'local',
	forceSelection: true,
	triggerAction: 'all',
	height:20
});

editPanel2 = Ext.extend(ns.gui.PEG.E, {
	baseTitle: "添加和编辑课程窗口",
	width: 550,
	frame: true,
	ethPairs:null,
	otherComponentId: [],
	initflag: true,
	selfName: '',
	
	saveCallback: function (option, success, response) {
		
		var result = Ext.decode(response.responseText);
		if(result.msg == false && result.msg.length>1)
		{
			Ext.Msg.alert("确认", result.msg);
		}
		g_subjectPanel.store.reload();
		editPanel.superclass.saveCallback.call(this, option, success, response);
	},
	
	onRender: function (ct, position) {
		editPanel.superclass.onRender.call(this, ct, position);
	
		this.on('hide', function () {
			this.record = null;
			this.initflag = true;
		}, this);
		
		this.add({
			layout: 'form',
			labelWidth: 80,	
			baseCls: 'x-plain',
			
			items: [{
				name: 'name',
				xtype: 'textfield',
				maxLength: 30,
				allowBlank: false,
				fieldLabel: this.selfName+"名称",
				anchor: '98%',
				vtype: 'entityname'
			},{
				name: 'desc',
				xtype: 'textfield',
				maxLength: 300,
				allowBlank: false,
				fieldLabel: this.selfName+"描述",
				anchor: '98%',
				vtype: 'entityname'
			},{
				name: 'id',
				xtype: 'textfield',
				maxLength: 300,
				allowBlank: true,
				fieldLabel: "标示",
				anchor: '98%',
				vtype: 'entityname',
				hidden:true
				
			},{
				name: 'level',
				xtype: 'textfield',
				maxLength: 300,
				allowBlank: true,
				fieldLabel: "level",
				anchor: '98%',
				vtype: 'entityname',
				hidden:true
				
			},
			comboSubject
			]
		});
	},
	show: function (action, record){	
		editPanel.superclass.show.call(this, action, record);
	}
});

var g_subjectPanel = new myGridPanel({
	name: 'versionPanel',
	deleteUrl: '/nbc_schoolyard/index.php/AdminChannelconfig/administrationde',
	
	store: new Ext.data.GroupingStore({
		autoLoad: true,
		reader: reader,
                Record: Record,
		//sortInfo:{field: 'id', direction: "ASC"},
		url: '/nbc_schoolyard/index.php/AdminChannelconfig/getadministration',
		listeners: {
			load: function () {
				
			},
			scope: this
		}
	}),
	
});
 
var g_edit1 = new editPanel({
	url: '/nbc_schoolyard/index.php/AdminChannelconfig/addadmin',
	selfName: '行政分类'
});

var g_edit2 = new editPanel2({
	url: '/nbc_schoolyard/index.php/AdminChannelconfig/addSonadmin',
	selfName: '行政子分类'
});
   
</script>
<script>

    Ext.onReady(function () {
        g_subjectPanel.render("class");
    });

</script>

    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
