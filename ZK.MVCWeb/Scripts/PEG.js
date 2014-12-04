Ext.namespace('ns.gui');

ns.gui.PEG = Ext.extend(Ext.grid.GridPanel, {
	//whether double click row to show edit window
	rowdblclickToEdit: true,
	//show confirm dialog when delete items
	confirmDelete: true,
	confirmTitle: L('Please Confirm'),
	confirmDelMsg: L('Do you want to delete selected item(s)?'),
	showApply: true,
	stripeRows: true,
	viewConfig: { emptyText: L('empty content') },

	deleteUrl: './delete',

	initComponent: function () {
		if (!this.popupEditor) {
			var ec;
			if (!this.editorClass)
				ec = ns.gui.PEG.E;
			else
				ec = this.editorClass;
			this.popupEditor = new ec(this.editorConfig);
		}

		//add by Junwei@2009-12-21, add more events
		this.addEvents('savesuccess','showapply', 'selectionchange','deletesuccess');

		ns.gui.PEG.superclass.initComponent.call(this);
	},

	initEvents: function () {
		ns.gui.PEG.superclass.initEvents.call(this);

		this.on('showapply', function () {
			if (this.showApply){
				ns.gui.showApply();
			}
		}, this);

		this.popupEditor.on('savesuccess', this.onSaveSuccess, this);
		
		this.on('cellclick', this.onCellClick, this);
		this.on('rowdblclick', this.onRowDblClick, this);
		
		//add selectionchange event for SelectionModel by Junwei@2009-12-21
		this.getSelectionModel().on('selectionchange', this.onSelectionChange, this);
	},
	
	//add by Junwei@2009-12-21, create a special Popup Editor
	createPopupEditor: function(editorClass,editorConfig) {
		var editor = new editorClass(editorConfig);
		editor.on('savesuccess', this.onSaveSuccess, this);
		return editor;
	},

	onCellClick: function (grid, row, col, e) {
		var record = grid.getStore().getAt(row);

		if (e.getTarget('td.peg-editor-trigger', 3) && e.getTarget('span', 0)) {
			this.onEdit(record);
		} else {
		}
	},

	onRowDblClick: function (grid, row, e) {
		if (!this.rowdblclickToEdit) return;	//add by Junwei@2009-12-21, rowdblclickToEdit property
		if (e.getTarget('.ux-maximgb-treegrid-elbow-active', 3) ||
			e.getTarget('td.peg-editor-trigger', 3) && e.getTarget('span', 0) || 
			e.getTarget('.peg-cell-icon-active', 3) ||
			e.getTarget('.x-grid3-td-expander', 3))
			return;
		var record = grid.getStore().getAt(row);
		this.onEdit(record);
	},
	
	//add by Junwei@2009-12-21, selectionchange 
	onSelectionChange: function(sm) {
		this.fireEvent('selectionchange', this, sm);
	},

	onEdit: function (record) {
		var action;

		if (!record) {
			record = new this.store.Record({});
			action = 'add';
		} else {
			action = 'edit';
		}
		this.popupEditor.show(action, record);
	},

	onDelete: function () {
		record = this.getSelectionModel().getSelections();

		if (!record.length) {
			return;
		}
		//add by xiao li 2011.05.19
		if(this.forceDelete){
			this.doDelete(record);
		}else{
			if (this.confirmDelete) {
				Ext.Msg.confirm(this.confirmTitle, this.confirmDelMsg, function (button) {
					if (button != 'yes')
						return;
		
					//extract doDelete func by Junwei@20091223
					this.doDelete(record);
				}, this);
			} else {
				this.doDelete(record);
			}
		}
	},
	
	doDelete: function(records) {
		//add by xiao li 2011.05.19
		if(this.storeLocal){
			var rlen = records.length;
			for(var i = 0 ; i<rlen;i++){
				this.store.remove(records[i]);
			}
			return;
		}
		this.showMask();

		var id = [],rlen = records.length;
		for (var i = 0; i < rlen; ++i ) {
			id[i] = records[i].get('id');
		}

		this.request({
			url: this.deleteUrl,
			record: records,
			params: {
				id: Ext.encode(id)
			},
			innerCallback: this.onDeleteCallback,
			innerScope: this
		});
	},
	
	onDeleteCallback: function (response, option) {
		var rlen = option.record.length;
		for (var i = 0; i < rlen; ++i) {
			this.store.remove(option.record[i]);
		}
		if(!this.storeLocal){
			if (option.record.length)
				this.fireEvent('showapply');
		}
		this.fireEvent('deletesuccess',option.record);
	},

	onSaveSuccess: function (action, record) {
		if (action == 'add') {
			if (typeof this.getStore().reader.meta.id != 'undefined')
				record.id = record.get(this.getStore().reader.meta.id)
			this.getStore().add([record]);
		}
		if(!this.storeLocal){
			this.fireEvent('showapply');
		}
		this.fireEvent('savesuccess');
	},

	request: function (options) {
		this.showMask();

		var opt = {
			callback: this.requestCallback,
			scope: this
		};

		Ext.apply(opt, options)

		Ext.Ajax.request(opt);
	},

	requestCallback: function (options, success, response) {
		if (success) {
			try {
				var r = Ext.decode(response.responseText);

				if (r.success) {
					if (typeof options.innerCallback == 'function') {
						options.innerCallback.call(options.innerScope || window, r, options);
					}
				} else {
					Ext.Msg.alert(L('Error'), L(r.msg));
				}
			} catch (e) {
				if (ns.gui.DEBUG) {
					ns.gui.logger.log(e);
					ns.gui.logger.log(response.responseText);
				}
			}
		} else {
			Ext.Msg.alert(
				L('Error'),
				L('Connection Error'));
		}
		this.hideMask();
	},

	onRender: function () {
		ns.gui.PEG.superclass.onRender.apply(this, arguments);
		this.mask = new Ext.LoadMask(this.bwrap, {
			msg: L('Please wait...')
		});
	},
	
	showMask: function() {
		if (this.mask) {
			this.mask.show();
		}
	},
	
	hideMask: function() {
		if (this.mask) {
			this.mask.hide();
		}
	}
});

ns.gui.PEG.E = Ext.extend(Ext.Window, {
	// not really useful
	width: 400, height: 'auto', focusFirst: true,
	// fix to within the window
	constrain: true, resizable: false, modal: true,plain: true,
	closeAction: 'hide',
	baseTitle: '',
	url: './save',
	bodyStyle: 'padding: 5px',
	/**
	 * @description config to Put not form elements's values into request parameters , add by xiao li 2011.05.20
	 * @example
	 * 		notFormParams:{
	 *			scopes:{type:'grid'}
	 *		}
	 */
	notFormParams:{},
	initComponent: function () {
		if (!this.hideButton){
			this.buttons = [{
					text: L('Ok'),
					minWidth: 70,
					handler: this.onSave,
					scope: this
				}, {
					text: L('Cancel'),
					minWidth: 70,
					handler: this.onCancel,
					scope: this
			}];
		}

		//move from PolicyPEG by Junwei@2009-12-21, add more events
		this.addEvents('savesuccess','beforeload','dataload','beforevalidate','validate','aftersave');
		
		ns.gui.PEG.E.superclass.initComponent.call(this);
		
		//move from PolicyPEG by Junwei@2009-12-21, set readonly option
		if (this.readOnly) {
			this.readOnly = false;
			this.setReadOnly(true);
		}
	},

	initEvents: function () {
		ns.gui.PEG.E.superclass.initEvents.call(this);

		this.on('hide', function () {
			this.record = null;
		}, this);
	},

	loadRecord: function (record) {
		if (this.readOnly) this.setReadOnly(false);
		
		this.fireEvent('beforeload',this,record);
		
		var f = this.getAllFields(), flen = f.length;

		for (var i = 0; i < flen; ++i) {
			var n = f[i].name;
			if (n) {
				var v = record.get(n);
				if (v != null) {
					f[i].setValue(v);
				}
			}
		}
		
		this.fireEvent("dataload",this,record);
		
		if (!ns.gui.Utility.isNullOrUndefined(record.get('readonly')))
			this.setReadOnly(record.get('readonly')==true);
	},
	
	setReadOnly: function(readOnly) {
		if (this.readOnly==readOnly) return;
		this.readOnly = readOnly;
		this.updateWindowTitle();
		if (readOnly) {
			this.buttons[0].hide();
			Ext.each(this.getAllFields(),function(item){ns.gui.Utility.setFieldReadOnly(item,true);});
		} else {
			this.buttons[0].show();
			Ext.each(this.getAllFields(),function(item){ns.gui.Utility.setFieldReadOnly(item,false);});
		}
	},

	getValues: function () {
		var f = this.getAllFields(),flen = f.length, v = {};

		for (var i = 0; i < flen; ++i) {
			var name = f[i].name;
			if (name) {
				var value = f[i].getValue();
				if (f[i] instanceof Ext.form.Radio) {
					if (value) {
						v[name] = f[i].inputValue;
					}
				} else if (f[i] instanceof Ext.form.RadioGroup && value.inputValue) {
					v[name] = value.inputValue;
				} else if (f[i] instanceof Ext.form.CheckboxGroup && value instanceof Array) {
					v[name] = [];
					for (var j = 0; j < value.length; j++) {
						v[name].push(value[j].inputValue);
					}
				} else {
					v[name] = value;
				}
			}
		}

		return v;
	},
	getFieldByName: function(name){
		var f = this.getAllFields(),flen = f.length;
		for (var i = 0; i < flen; ++i) {
			if(f[i].name == name){
				return f[i];
			}
		}
	},
	updateRecord: function (resp) {// modify by xiao li 10.01.11,for bug 0013678
		var v = this.getValues();
		for (var i in v) {
			this.record.set(i, v[i]);
		}
	},

	getFieldNames: function () {
		var f = this.getAllFields(), r = [],flen = f.length;
		for (var i = 0; i < flen; ++i) {
			r[r.length] = f[i].name;
		}

		return r;
	},

	validateAll: function () {
		//beforevalidate
		this.fireEvent("beforevalidate",this);
		
		var f = this.getAllFields(), flen = f.length, ret = true;

		for (var i = 0; i < flen; ++i) {
			ret &= f[i].isValid(false);
		}
		//add by zhaoshuai  for bug 0031956
		if(!ret){
			this.formErrorMsg=L('There are error(s) in the form.');
		}
		//add by xiao li 
		for(var a_el in this.notFormParams){
			var tmp_obj = this.notFormParams[a_el];
			switch(tmp_obj.type){
				case 'grid':
					var list_st = tmp_obj.obj.getStore();
					if(list_st.getCount()==0){
						this.setFormErrorMsg(L('List can not empty'));
						return false;
					}
					break;
			}
		}
		return ret && this.checkFieldsBeforeSave() && this.fireEvent('validate',this);
	},
	
	/*
	 * add by xiao li 09.12.14, for check the field when clicking ok
	 *  checkFieldsOnOk =[{
	 *	id:'xx', // need check field id
	 *	validator:function(o){curField,action,editorwin.record,editorwin}// the check event
	 *	}]
	 */
	checkFieldsBeforeSave: function(){
		if(this.checkActionOnOk){
			if(this.checkActionOnOk(this.getValues(),this)){
				return true;
			}else{
				return false;
			}
		}
		
		if(this.checkFieldsOnOk){
			var chk = 0, cfLen = this.checkFieldsOnOk.length;
			for (var i = 0; i<cfLen; i++){
				if (this.checkFieldsOnOk[i].id) {
					var fId = this.checkFieldsOnOk[i].id;
					if(fId instanceof Array){
						var flen = fId.length;
						for (var j = 0; j<flen;j++){
							var curField = this.findById(fId[j]);
							if(!this.checkFieldsOnOk[i].validator(curField,this.action,this.record,this)){
								chk = 1;
							}
						}
					}else{
						var curField = this.findById(fId);
						if(!this.checkFieldsOnOk[i].validator(curField,this.action,this.record,this)){
							chk = 1;
						}
					}
				}
				if (this.checkFieldsOnOk[i].name) {
					var fId = this.checkFieldsOnOk[i].name;
					if(fId instanceof Array){
						var flen = fId.length;
						for (var j = 0; j<flen;j++){
							var curField = this.getFieldByName(fId[j]);
							if(!this.checkFieldsOnOk[i].validator(curField,this.action,this.record,this)){
								chk = 1;
							}
						}
					}else{
						var curField = this.getFieldByName(fId);
						if(!this.checkFieldsOnOk[i].validator(curField,this.action,this.record,this)){
							chk = 1;
						}
					}
				}
			}
			
			if(chk == 1) return false;
		}
		return true;
	},
	//add by xiao li,2010.07.23
	formErrorMsg:L('There are error(s) in the form.'),
	setFormErrorMsg:function(msg, append){
		if(!msg) msg = L('There are error(s) in the form.');
		if (append) {
			this.formErrorMsg += msg;
		} else {
			this.formErrorMsg = msg;
		}
	},
	onSave: function () {
		//changed by Junwei@20091221, for allow throw Exception in field's isValid function
		try {
			var valid = this.validateAll();
		} catch (e) {
			this.alertError(e);
			return;
		}
		if (valid) {
			//extract doSave func by Junwei@20091223
			this.doSave();
		} else {
//			Ext.Msg.alert(
//				L('Error'),
//				this.formErrorMsg);
			Ext.Msg.show({
				title: L('Prompt'), msg: this.formErrorMsg,
				buttons: Ext.Msg.OK, icon: Ext.MessageBox[this.errType?this.errType:'ERROR'], minWidth:200 
			});
		}
	},
	
	/**
	 * alert Error
	 * @param {Error} e
	 */
	alertError: function(e) {
		if (typeof e=='string') {
			Ext.Msg.alert(L('Error'),L(e));
		} else if (e.name && e.message) {
			Ext.Msg.alert(L('Error'),L(['{name}: {message}',e]));
		} else {
			Ext.Msg.alert(L('Error',e));
		}
	},

	onCancel: function () {
		this.processFieldBeforeCancel(this);//add by xiao li
		this.hide();
	},
	//add by xiao li
	processFieldBeforeCancel:function(_me){
		
	},
	//extract from onSave func by Junwei@20091223
	doSave: function() {
		//config input data only store at local,add by xiao li 2011.05.19
		if(this.storeLocal){
			var r = this.record;
			r.beginEdit();
			var data = this.getValues() || {};
			for (var k in data) {
				r.set(k, data[k]);
			}
			r.commit();
			this.fireEvent('savesuccess', this.action, r);
			this.hide();
			return;
		}
		this.showMask();
		// put not form elements's value to request, add by xiao li 2011.05.20
		var data = {};
		for(var a_el in this.notFormParams){
			var tmp_obj = this.notFormParams[a_el];
			switch(tmp_obj.type){
				case 'grid':
					var list_st = tmp_obj.obj.getStore();
					var v_list = [];
					for (var i = 0, ilen = list_st.getCount(); i < ilen; ++i) {
						v_list[i] = list_st.getAt(i).data;
					}
					data[a_el] = v_list;
					break;
			}
		}
		Ext.apply(data,this.getValues());
		var params = {
				data: Ext.encode(data)
		};
		if (this.customParams){
			Ext.apply(params,this.customParams);
		}
		Ext.Ajax.request({
			url: this.url,
			params: params,
			callback: this.saveCallback,
			scope: this
		});
	},
	saveCallback: function (option, success, response) {
		if (success) {
			try {
				var result = Ext.decode(response.responseText);

				if (result.success) {
					var r = this.record;

					r.beginEdit();
					this.updateRecord(result); // modify by xiao li 10.01.11,for bug 0013678

					var data = result.data || {};
					for (var k in data) {
						r.set(k, data[k]);
					}
					r.commit();
					this.fireEvent('savesuccess', this.action, r, result);
					this.fireEvent('aftersave',this.action, r);
					this.hide();
				} else {
					Ext.Msg.alert(L('Error'), L(result.msg));
				}
			} catch (e) {
				if (ns.gui.DEBUG) {
					ns.gui.logger.log(e);
					ns.gui.logger.log(response.responseText);
				}
			}
		} else {
			Ext.Msg.alert(
				L('Error'),
				L('Connection Error'));
		}
		this.hideMask();
	},

	//add by Junwei@2009-12-23, wmask init onrender
	onRender: function () {
		ns.gui.PEG.E.superclass.onRender.apply(this, arguments);
		if (!this.wmask) {
			this.wmask = new Ext.LoadMask(this.el, {
				msg: L('Please wait...')
			});
		}
	},
	
	showMask: function() {
		if (this.wmask) this.wmask.show();
	},
	
	hideMask: function() {
		if (this.wmask) this.wmask.hide();
	},

	show: function (action, record, title) {
		this.setFormErrorMsg();//add by xiao li,2010.07.23
		this.record = record;
		this.action = action;
        if(!this.rendered){
            this.render(Ext.getBody());
        }

		this.reset();
		if (action == 'add') {
			this.setTitle(title?title:L('Create'));
		} else if (action == 'edit') {
			this.setTitle(title?title:L('Edit'));
		} else {
			alert('debug me');
		}
		//load not form values
		for(var a_el in this.notFormParams){
			var tmp_obj = this.notFormParams[a_el];
			switch(tmp_obj.type){
				case 'grid':
					var el_st = tmp_obj.obj.getStore();
					el_st.removeAll();
					if (action == 'edit') {
						var el_data = this.record.get(a_el),ellen = el_data.length;
						for(var l =0; l< ellen;l++){
							el_st.add(new Ext.data.Record(el_data[l]));
						}
					}
				break;
			}
		}
		if (record) {
			this.loadRecord(this.record);
		}
		this.processField(this,this.record,action);	
		ns.gui.PEG.E.superclass.show.call(this);
		this.setFocus();
	},
	//add by xiao li 2010.06.25 process after all default action and befroe show
	processField:function(_this,record,action){
		
	},
	setFocus: function(delay){
		if(this.focusFirst){
			var fields = this.findByType('textfield');
			if(fields.length>0){
				if (!delay){
					var delay = 300;
				}
				fields[0].focus(this.foucsSelect,delay);
			}
		}
	},
	updateWindowTitle: function() {
		if (this.readOnly) {
			this.setTitle(L('ReadOnly Object'));
		} else if (this.action == 'add') {
			this.setTitle(L('Create'));
		} else if (this.action == 'edit') {
			this.setTitle(L('Edit'));
		}
	},

	setTitle: function (title) {
		if (this.fixTitle) {
			title = this.fixTitle;
		} else {
			if( this.baseTitle ) {
				title = this.baseTitle + ' - ' + title;
			}else{
				title
			}
		}
		ns.gui.PEG.E.superclass.setTitle.call(this, title);
	},
	//add by xiao li 2010.06.24
	setBaseTitle: function(title){
		this.baseTitle = title;
	},
	reset: function () {
		if (!this.rendered)
			return;

		var f = this.getAllFields(),flen = f.length;
		for (var i = 0; i < flen; ++i) {
			if (f[i].name) {
				f[i].reset();
			}
		}
	},
	
	//move from PolicyPEG by Junwei@20091218
	getAllFields: function() {
		if(!this.fields) {
			this.fields = this.findBy(function(f) {
				if (f.name && (f.isFormField || f.isSpField)) {
					return true;
				} else {
					return false;
				}
			});
		}
		return this.fields;
	},
	
	getFieldValue: function(name) {
		var values = this.getValues();
		return values[name];
	},
	//add by xiao li 2011.05.19
	setFieldValue: function(name,value){
		this.getFieldByName(name).setValue(value);
	},
	clearAllInvalid:function(){
		var fields = this.getAllFields(),flen = fields.length;
		for(var i=0;i<flen;i++){
			var f = fields[i];
			if(f.clearInvalid){
				f.clearInvalid();
			}
		}
	}

});

ns.gui.Utility = {
	isNullOrUndefined: function(obj) {
        return (typeof obj == 'undefined' || obj == null );
    },
    isFunction: function(f){
        return typeof f == 'function';
    },
    setFieldReadOnly: function(f,value) {
    	if (!f) return;
    	//readonly css
	    if (value) {
        	f.addClass('x-form-readonly');
        } else {
        	f.removeClass('x-form-readonly');
        }
    	if (ns.gui.Utility.isFunction(f.setReadOnly)) {
    		//if f has readonly function, just call it
    		f.setReadOnly(value);
    		return;
    	}
		if(f.readOnly)//if readonly is set in the Ext way, like in cfg object, do not take any action.
            return;
        if (f._ns_readOnly==value) return;
        
        if (ns.gui.Utility.isNullOrUndefined(f._ns_readOnly)) {
        	if(ns.gui.Utility.isFunction(f.expand))
	            f.expand = f.expand.createInterceptor(function(){
	                return !f._ns_readOnly;
	            });
	        if(ns.gui.Utility.isFunction(f.onTriggerClick))
	            f.onTriggerClick = f.onTriggerClick.createInterceptor(function(){
	                return !f._ns_readOnly;
	            });
	        if(ns.gui.Utility.isFunction(f.onClick))
	            f.onClick = f.onClick.createInterceptor(function(){
	                if(f._ns_readOnly){
	                    this.el.dom.checked = f.checked;
	                }
	                return !f._ns_readOnly;
	            });
	        if(ns.gui.Utility.isFunction(f.setValue) && f instanceof Ext.form.Checkbox)
	            f.setValue = f.setValue.createInterceptor(function(){
	                return !f._ns_readOnly;
	            });
        }
        f._ns_readOnly = value;
        if(f.rendered){
	        if(ns.gui.Utility.isNullOrUndefined(f.editable) || f.editable === true){
	            var el = f.getEl();
	            el.dom.setAttribute('readOnly', value);
	            el.dom.readOnly = value;
	        }
        } else {
            f.readOnly = value;
        }
	}
}