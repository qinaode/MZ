Ext.enableFx = false;
Ext.BLANK_IMAGE_URL = '/js/extjs-new/resources/images/default/s.gif';
Ext.Msg.minWidth = 200;

//fix NumberField 1000000000000000000000(1E21) show as 121 by Junwei
(function() {
	var oldRender = Ext.form.NumberField.prototype.onRender;
	Ext.apply(Ext.form.NumberField.prototype, {
		onRender : function(ct, position){
			oldRender.call(this,ct,position);
			if (this.maxLength!=Number.MAX_VALUE)
				this.el.dom.maxLength = this.maxLength;
		}
	});
	//add by xiao li 2010.07.28
	Ext.apply(Ext.form.TextField.prototype, {
		filterKeys : function(e){
	        // special keys don't generate charCodes, so leave them alone
	        if(e.ctrlKey || e.isSpecialKey()|| e.getKey() == e.DELETE){
	            return;
	        }
	        
	        if(!this.maskRe.test(String.fromCharCode(e.getCharCode()))){
	            e.stopEvent();
	        }
	    }
	});
})();

Ext.namespace('ns.gui');

ns.gui.showApply = function () {
	if (parent && typeof parent.Show_warning == 'function') {
		parent.Show_warning(1);
	}else if(parent.parent && typeof parent.parent.Show_warning == 'function'){
		parent.parent.Show_warning(1);
	}
}
ns.gui.lib = {
	ip2long: function (ip) {
		ip = ip.split('.');
		return parseInt(ip[0])*Math.pow(2, 24)+parseInt(ip[1])*Math.pow(2, 16)+parseInt(ip[2])*Math.pow(2, 8)+parseInt(ip[3]);
	},

	long2ip: function(longIp) {
		var ip = '';

		ip += (longIp>>>24).valueOf();
		ip += ".";
		ip += ((longIp&0x00FFFFFF)>>>16).valueOf();
		ip += ".";
		ip += ((longIp&0x0000FFFF)>>>8).valueOf();
		ip += ".";
		ip += (longIp&0x000000FF).valueOf();

		return ip;
	},

	updateRecord: function (record, fields, data) {
		if (fields) {
			for (var i = 0; i < fields.length; ++i) {
				var name = fields[i].name;
				if (name) {
					if (fields[i] instanceof Ext.form.Radio) {
						if (fields[i].getValue())
							record.set(name, fields[i].inputValue);
					} else {
						record.set(name, fields[i].getValue());
					}
				}
			}
		} else if (data) {
			for (var key in data) {
				record.set(key, data[key]);
			}
		}
	},

	loadRecord: function (record, fields) {
		for (var i = 0; i < fields.length; ++i) {
			var n = fields[i].name;
			if (n) {
				var v = record.get(n);
				if (v != null) {
					fields[i].setValue(v);
				}
			}
		}
	},

	validateFields: function (fields) {
		var ret = true;
		for (var i = 0; i < fields.length; ++i)
			ret &= fields[i].isValid();

		return ret;
	},

	resetFields: function (fields) {
		for (var i = 0; i < fields.length; ++i) {
			fields[i].reset();
		}
	},

	getValues: function (fields) {
		var v = {};

		for (var i = 0; i < fields.length; ++i) {
			var name = fields[i].name;
			if (name) {
				if (fields[i] instanceof Ext.form.Radio) {
					if (fields[i].getValue())
						v[name] = fields[i].inputValue;
				} else {
					v[name] = fields[i].getValue();
				}
			}
		}

		return v;
	}
};

ns.gui.lib.Map = function (getKey) {
	this.map = {};

	if (getKey)
		this.getKey = getKey;
};
ns.gui.lib.Map.prototype = {
	get: function (key) {
		return this.map[this.getKey(key)];
	},
	set: function (key, value) {
		var key = this.getKey(key);
		if (value !== null)
			this.map[key] = value;
		else
			delete this.map[key];
	},
	getKey: function (keyObject) {
		return keyObject.id
	}
};

(function () {
var ipRe = /^((\d|[1-9]\d|1\d\d|2([0-4]\d|5[0-5]))\.){3}([1-9]|[1-9]\d|1\d\d|2([0-4]\d|5[0-5]))$/;
var ipRe1 = /^((\d|[1-9]\d|1\d\d|2([0-4]\d|5[0-5]))\.){3}(\d|[1-9]\d|1\d\d|2([0-4]\d|5[0-5]))$/;
var cidrRe = /^((\d|[1-9]\d|1\d\d|2([0-4]\d|5[0-5]))\.){3}(\d|[1-9]\d|1\d\d|2([0-4]\d|5[0-5]))\/(\d|[12]\d|3[0-2])$/;
var macRe = /^([0-9A-Fa-f]{2}:){5}[0-9A-Fa-f]{2}$/;

Ext.apply(Ext.form.VTypes, {
	ip: function (v) {
		return ipRe.test(v);
	},
	ipText: L('Invalid IP Address'),
	ipMask: /[\d.]/,

	iprange: function (v, field) {
		if (ipRe1.test(v)) {
			if (field.startField) { // this is an "end" field
				var sv = field.startField.getValue();
				if (ipRe1.test(sv)) {
					return ns.gui.lib.ip2long(v) > ns.gui.lib.ip2long(sv);
				} else {
					return true;
				}
			} else {
				return true;
			}
		} else {
			return false;
		}
	},
	iprangeText: L('Invalid IP range'),
	iprangeMask: /[\d.]/,

	cidr: function (v) {
		return cidrRe.test(v);
	},
	cidrText: L('Invalid Cidr'),
	cidrMask: /[\d.\/]/,

	ipv6: function(v) {
		var result = /:/.test(v) && v.match(/:/g).length < 8 && 
		( /::/.test(v)
		? (v.match(/::/g).length==1 && /^::$|^(::)?([\da-f]{1,4}(:|::))*[\da-f]{1,4}(:|::)?$/i.test(v))
		: /^([\da-f]{1,4}:){7}[\da-f]{1,4}$/i.test(v) );
		return result || /^::(ffff:)?((\d|[1-9]\d|1\d\d|2([0-4]\d|5[0-5]))\.){3}(\d|[1-9]\d|1\d\d|2([0-4]\d|5[0-5]))$/i.test(v);
	},
	ipv6Text: L('Invalid IPv6 Address'),
	ipv6Mask: /[\da-f:.]/i,
	
	ipv6cidr: function(v) {
		var vv = v.split("/");
		if (vv.length==2) {
			return Ext.form.VTypes.ipv6(vv[0]) && /^\d+$/.test(vv[1]) && vv[1]>0 && vv[1]<=128;
		}
		return false;
	},
	ipv6cidrText: L('Invalid IPv6 Cidr'),
	ipv6cidrMask: /[\da-f:.\/]/i,
	
	mac: function (v) {
		return macRe.test(v);
	},
	macText: L('Invalid MAC address'),
	macMask: /[0-9A-Fa-f:]/,

	entityname: function (v) {
		rs= /[^\s\u3000]/.test(v);
		if(rs){
			var reg = /([\*\$&\/\\@~%!\/])+/;
			rs = !reg.test(v);
		}
		return rs;
		
	},
	entitynameText: L('Name Error'),

	ceil: function (v, field) {
		if (field.getRawValue().length && field.rateId) {
			var rate = Ext.getCmp(field.rateId);
			return rate.getRawValue().length && parseInt(v) >= parseInt(rate.getValue());
		} else {
			return true;
		}
	},
	ceilText: L('tunnel rate&ceil'),
	value2or1:function(v,field){
		var rid = field.relationId;
		if(rid){
			var robj = Ext.getCmp(rid);
			robj.clearInvalid();
			field.clearInvalid();
		}
		return true;
	},
	value2or1Text:L('Per user limit error'),
	
	multiemail: function(v,field){
		 var email = /^(\w+)([-+.][\w]+)*@(\w[-\w]*\.){1,5}([A-Za-z]){2,4}$/;
		 var ips = v.split(','),iplen = ips.length;
		 for(var i = 0; i<iplen;i++){
			 if(!email.test(ips[i])){
				 return false;
			 }
		 }
		 return true;
	},
	multiemailText:L('multi email error'),
	intnumber:function(v){
		if(v==0){
			return true;
		}
		var reg = /^[1-9]\d*$/;
		return reg.test(v);
	},
	intnumberText:L('not decimal'),
	enstring: function(v){
		var reg = /^[A-Za-z0-9]+$/;
		return reg.test(v);
	},
	enstringText: L('English String and Number'),
	enzhstring:function(v){
		var reg = /^[A-Za-z0-9\u4E00-\u9FA5]+$/;
		return reg.test(v);
	},
	enzhstringText: L('English and Chinese String and Number'),
	multiIp: function(v){
		var spliter = Ext.isIE?"\r\n":"\n"
		var ips = v.split(spliter);
		for(var i=0 ;i<ips.length; i++){
			var ip = ips[i];
			if(!ipRe.test(ip)) {
				return false;
			}else{
				for(var j=0; j<ips.length; j++){
					if(ip == ips[j] && i!=j){
						return false;
					}
				}
			}
		}
		return true;
	},
	multiIpText: L('multi ip error msg')
});
})();


ns.gui.DEBUG = 1;
//ns.gui.THROW = 1;

ns.gui.logger = function () {
	var msg = [];

	return {
		msg: msg,
		log: function (mm) {
			msg[msg.length] = mm;
			if (ns.gui.THROW)
				throw mm;
		}
	};
}();

Ext.namespace('ns.gui.util');
ns.gui.util.Map = function (config) {
	Ext.apply(this, config || {});
	this.map = {};
}
ns.gui.util.Map.prototype = {
	get: function (key) {
		var strKey = this.keyToString(key);
		if (typeof this.map[strKey] != 'undefined')
			return this.map[strKey];
		else
			return null;
	},
	set: function (key, value) {
		var strKey = this.keyToString(key);
		if (value !== null)		
			this.map[strKey] = value;
		else
			delete this.map[strKey];
	},
	keyToString: function (key) {
		return key;
	},
	clear: function () {
		this.map = {};
	}
};

Ext.namespace('ns.gui.data');

ns.gui.data.StoreRefProxy = Ext.extend(Ext.data.MemoryProxy, {
	//@Override
    load: function (params, reader, callback, scope, arg) {
		try {
			var r = [];
			this.data.each(function (ele) {
				r[r.length] = ele;
			});

			callback.call(scope, {records: r}, arg, true);
		} catch (e) {
			this.fireEvent("loadexception", this, arg, null, e);
			callback.call(scope, null, arg, false);
		}
    }
});

/*
 * This store should not be accessed directly in anyway other than a probable kick-start in the case of the referenced store has already loaded when this store is created.
 * This store will proxy all the referenced store's events as if the component is using the referenced store itself.
 * A filter can be provided.
 */
ns.gui.data.ProxyStore = function (config) {
	config.reader = config.refStore.reader;
	config.sortInfo = config.refStore.sortInfo;
	config.proxy = new ns.gui.data.StoreRefProxy(config.refStore);
	ns.gui.data.ProxyStore.superclass.constructor.call(this, config);
	
	this.refStore.on('load', this.onRefLoad, this);
	//baseStore
	if (this.relayEvents !== false) {
		this.refStore.on('update', this.onRefUpdate, this);
		this.refStore.on('add', this.onRefAdd, this);
		this.refStore.on('remove', this.onRefRemove, this);
		this.refStore.on('datachanged', this.onRefChanged, this);
		this.refStore.on('loadexception', function (store, arg, data, e) {
			this.fireEvent("loadexception", this, arg, data, e);
		}, this);
	}

	this.refMap = new ns.gui.util.Map({
		keyToString: function (record) {
			return record.id;
		}
	});

	if (config.refFilter) {
		this.setFilter(config.refFilter);
	}
}
Ext.extend(ns.gui.data.ProxyStore, Ext.data.Store, {
	load : function(options){ //add by xiao li 2010.12.28 for ext 3.x.x version
	    options = options || {};
	    if(this.fireEvent("beforeload", this, options) !== false){
	        this.storeOptions(options);
	        var p = Ext.apply(options.params || {}, this.baseParams);
	        if(this.sortInfo && this.remoteSort){
	            var pn = this.paramNames;
	            p[pn["sort"]] = this.sortInfo.field;
	            p[pn["dir"]] = this.sortInfo.direction;
	        }
	        this.proxy.load(p, this.reader, this.loadRecords, this, options);
	        return true;
	    } else {
	      return false;
	    }
	},
	/**
	 * update by Junwei@20091120
	 * filter=false, to clear filter
	 * filter can be a function
	 * 
	 * @param {Array|Object|Function} filter
	 */
	setFilter: function (filter) {
		if (Ext.isArray(filter)) {
			var refKey = filter[0], refValue = filter[1];
			this.refFilter = function (record) {
				return record.get(refKey) == refValue;
			}
		} else if ( filter ) {
			if (typeof filter=='function') {
				this.refFilter = filter;
			} else if (typeof filter.fn == 'function') {
				//this.reFilterConfig = filter;
				var scope = filter.scope ? filter.scope : this;
				this.refFilter = function (record) {
					return filter.fn.call(scope, record);
				}
			}
		} else {
			//clear filter
			this.refFilter = function(record) {return true;}
		}
	},

	refFilter: function (record) {
		return true;
	},

	onRefLoad: function (rs, record, options) {
		var tmp = Ext.apply({}, options);
		delete tmp.callback;
		this.load(tmp);
	},
	
	onRefUpdate: function (rs, record, operation) {
		if (operation == Ext.data.Record.COMMIT) {
			var lr = this.refMap.get(record);
			if (lr) {
				this.syncRecord(record, lr);
			}
		}
	},

	onRefAdd: function (rs, record, index) {
		var toAdd = [];
		for (var i = 0; i < record.length; ++i) {
			if (this.refFilter(record[i])) {
				var r = this.copyRef(record[i]);
				this.refMap.set(record[i], r);
				toAdd[toAdd.length] = r;
			}			
		}

		this.add(toAdd);
	},

	onRefRemove: function (rs, record, index) {
		var lr = this.refMap.get(record);
		if (lr) {
			this.refMap.set(record, null);
			this.remove(lr);
		}
	},

	onRefChanged: function (rs) {
		this.fireEvent('datachanged', this);
		//alert('changed');
		//this.load();
	},

	syncRecord: function (src, dst) {
		dst.beginEdit();
		for (var key in src.data) {
			dst.set(key, src.data[key]);
		}
		dst.commit();
	},

	copyRef: function (record) {
		return record.copy();
	},

	loadRecords: function(o, options, success) {
        if (!options || options.add !== true) {
			this.refMap.clear();
		}

		var filtered = [];
		if (o) {
			var rs = o.records, r;
			for (var i = 0; i < rs.length; ++i) {
				if (this.refFilter(rs[i])) {
					r = this.copyRef(rs[i]);
					filtered[filtered.length] = r;
					this.refMap.set(rs[i], r);
				}
			}
		}

		ns.gui.data.ProxyStore.superclass.loadRecords.call(this, { records: filtered }, options, success);
	}
});

ns.gui.data.StoreProxy = Ext.extend(Ext.data.MemoryProxy, {
	//@Override
    load: function (params, reader, callback, scope, arg) {
		try {
			var or, r = [];
			if (params.filter) {
				or = this.data.query(params.filter[0], params.filter[1]);
			} else {
				or = this.data;
			}
			or.each(function (ele) {
				r[r.length] = ele.copy();
			});

			callback.call(scope, {records: r}, arg, true);
		} catch (e) {
			this.fireEvent("loadexception", this, arg, null, e);
			callback.call(scope, null, arg, false);
		}
    }
});

Ext.namespace('ns.gui.grid');

ns.gui.grid.renderer = {
	hsc: function (value) {
		if (value==null) return '';
		return value.toString().replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;');
	},

	clickable: function (value, metadata, record, rowIndex, colIndex, store) {
		metadata.css = 'peg-editor-trigger';
		return '<span>'+ns.gui.grid.renderer.hsc(value)+'</span>';
	},

	bool: function (value) {
		return value ? L('yes') : L('no');
	}
};

ns.gui.grid.RowExpander = function(config){
    Ext.apply(this, config);

    this.addEvents({
        beforeexpand : true,
        expand: true,
        beforecollapse: true,
        collapse: true
    });

    ns.gui.grid.RowExpander.superclass.constructor.call(this);

    if(this.tpl){
        if(typeof this.tpl == 'string'){
            this.tpl = new Ext.Template(this.tpl);
        }
        this.tpl.compile();
    }

    this.state = {};
    this.bodyContent = {};
};

Ext.extend(ns.gui.grid.RowExpander, Ext.util.Observable, {
    header: "",
    width: 20,
    sortable: false,
    fixed:true,
    menuDisabled:true,
    dataIndex: '',
    id: 'expander',
    lazyRender : true,
    enableCaching: false,

    getRowClass : function(record, rowIndex, p, ds){
        p.cols = p.cols-1;
        var content = this.bodyContent[record.id];
        if(!content && !this.lazyRender){
            content = this.getBodyContent(record, rowIndex);
        }
        if(content){
            p.body = content;
        }
        return this.state[record.id] ? 'x-grid3-row-expanded' : 'x-grid3-row-collapsed';
    },

    init : function(grid){
        this.grid = grid;

        var view = grid.getView();
        view.getRowClass = this.getRowClass.createDelegate(this);

        view.enableRowBody = true;

        grid.on('render', function(){
            view.mainBody.on('mousedown', this.onMouseDown, this);
        }, this);

		var ds = grid.getStore();
		view.on('refresh', function (v) {
			for (var i = 0, ilen = ds.getCount(); i < ilen; ++i) {
				var row = v.getRow(i);
				if (Ext.fly(row).hasClass('x-grid3-row-expanded')) {
					this.expandRow(row);
				}
			}
		}, this);
		view.on('rowupdated', function (v, row) {
			row = v.getRow(row);
			if (Ext.fly(row).hasClass('x-grid3-row-expanded')) {
				this.expandRow(row);
			}
		}, this);
    },

    getBodyContent : function(record, index){
        if(!this.enableCaching){
            return this.tpl.apply(record.data);
        }
        var content = this.bodyContent[record.id];
        if(!content){
            content = this.tpl.apply(record.data);
            this.bodyContent[record.id] = content;
        }
        return content;
    },

    onMouseDown : function(e, t){
		if(e.getTarget('.x-grid3-td-expander', 3)){
            e.stopEvent();
            var row = e.getTarget('.x-grid3-row');
            this.toggleRow(row);
        }
    },

    renderer : function(v, p, record){
        p.cellAttr = 'rowspan="2"';
        return '<div class="x-grid3-row-expander">&#160;</div>';
    },

    beforeExpand : function(record, body, rowIndex){
        if(this.fireEvent('beforeexpand', this, record, body, rowIndex) !== false){
            if(this.lazyRender){
                body.innerHTML = this.getBodyContent(record, rowIndex);
            }
            return true;
        }else{
            return false;
        }
    },

    toggleRow : function(row){
        if(typeof row == 'number'){
            row = this.grid.view.getRow(row);
        }
        this[Ext.fly(row).hasClass('x-grid3-row-collapsed') ? 'expandRow' : 'collapseRow'](row);
    },

    expandRow : function(row){
        if(typeof row == 'number'){
            row = this.grid.view.getRow(row);
        }
        var record = this.grid.store.getAt(row.rowIndex);
        var body = Ext.DomQuery.selectNode('tr:nth(2) div.x-grid3-row-body', row);
        if(this.beforeExpand(record, body, row.rowIndex)){
            this.state[record.id] = true;
            Ext.fly(row).replaceClass('x-grid3-row-collapsed', 'x-grid3-row-expanded');
            this.fireEvent('expand', this, record, body, row.rowIndex);
        }
    },

    collapseRow : function(row){
        if(typeof row == 'number'){
            row = this.grid.view.getRow(row);
        }
        var record = this.grid.store.getAt(row.rowIndex);
        var body = Ext.fly(row).child('tr:nth(1) div.x-grid3-row-body', true);
        if(this.fireEvent('beforecollapse', this, record, body, row.rowIndex) !== false){
            this.state[record.id] = false;
            Ext.fly(row).replaceClass('x-grid3-row-expanded', 'x-grid3-row-collapsed');
            this.fireEvent('collapse', this, record, body, row.rowIndex);
        }
    }
});

Ext.namespace('ns.gui.form');

ns.gui.form.LocalCombo = Ext.extend(Ext.form.ComboBox, {
	mode: 'local',
	triggerAction: 'all',
	editable: false,

	onStoreRemove: function (s, r, i) {
		var v = this.getValue();
		if (r.get(this.valueField) == v)
			this.reset();
	},

	onStoreUpdate: function (s, r) {
		var v = this.getValue();
		if (r.get(this.valueField) == v)
			this.setRawValue(r.get(this.displayField));
	},

	// @override
    bindStore : function(store, initial){
        if(this.store && !initial){
			if (this.store.un) {
				this.store.un('beforeload', this.onBeforeLoad, this);
				this.store.un('load', this.onLoad, this);
				this.store.un('loadexception', this.collapse, this);
				this.store.un('remove', this.onStoreRemove, this);
				this.store.un('update', this.onStoreUpdate, this);
			}
            if(!store){
                this.store = null;
                if(this.view){
                    this.view.bindStore(null);
                }
            }
        }
        if(store){
            this.store = Ext.StoreMgr.lookup(store);

            this.store.on('beforeload', this.onBeforeLoad, this);
            this.store.on('load', this.onLoad, this);
            this.store.on('loadexception', this.collapse, this);
			this.store.on('remove', this.onStoreRemove, this);
			this.store.on('update', this.onStoreUpdate, this);

            if(this.view){
                this.view.bindStore(store);
            }
        }
    }
});
Ext.reg('localcombo', ns.gui.form.LocalCombo);

ns.gui.form.LocalCombo.listTplWithButton = function(str,btn_id,itemCss){
	if(!itemCss){
		itemCss = "ml-combo-item";
	}
	if(btn_id){
		var tpl_str =  '<tpl for="."><div class="'+itemCss+'">' + str + '</div></tpl><div id='+btn_id+' style="height:20px;width:50px;cursor:pointer;margin:2px 0 0 10px;padding:2px 0 0 20px;font-size:12px" class ="button-icon-add">'+L('Add')+'</div>';
		var tpl = new Ext.XTemplate(tpl_str);
		tpl.compile();
		return tpl;
	}
}	

ns.gui.form.Collection = Ext.extend(Ext.form.Field, {
	defaultAutoCreate: {tag: 'div'}, autoScroll:true, 
	displayField: 'name', valueField: 'id',
	emptyText: L('click to set'), baseCls: 'form-clct', blankText : Ext.form.TextField.prototype.blankText,
	allowBlank : true,
	initComponent: function () {
		ns.gui.form.Collection.superclass.initComponent.call(this);
		this.emptyText = '<div class="dataview-empty">'+this.emptyText+'</div>';
		this.addEvents('containerclick');

		this.valueStore = new ns.gui.data.ProxyStore({
			refStore: this.store,
			refFilter: {
				fn: function (record) {
					return this.isSelected(record.get(this.valueField));
				},
				scope: this
			}
		});
	},

	isSelected: function (value) {
		if (this.value) { // Add by Junwei@20091127, fix some exception
			return this.value.indexOf(value) != -1;
		} else {
			return false;
		}
	},

	onRender: function (ct, position) {
		ns.gui.form.Collection.superclass.onRender.call(this, ct, position);
		this.el.on('click', this.onClick, this);

		if (!this.tpl) {
			var tpl = '<tpl for="."><div class="form-clct-item">{[ns.gui.grid.renderer.hsc(values.'+this.displayField+')]},</div></tpl>';
			this.tpl = new Ext.XTemplate(tpl);
			this.tpl.compile();
		}

		this.view = new Ext.DataView({
			store: this.valueStore, tpl: this.tpl, emptyText: this.emptyText,
			itemSelector: 'div.' + this.baseCls + '-item',
			selectedClass: this.baseCls + '-selected'
		});
		var p = new Ext.Container({
			renderTo: this.el, cls:this.baseCls, height: this.height, width: this.width,
			items: this.view
		});
	},

	onClick: function () {
		this.fireEvent('containerclick', this);
	},

	//@Override
	initValue: function () {
		if (this.value) {
			this.valueStore.reload();
			this.snapshot = [].concat(this.value);
		}
	},

	//@Override
	getRawValue: function() {
		// get see value add by xiao li 12.03.28
		if(this.value && this.store){
			var vlen = this.value.length, rawValue = [];
			for(var i=0; i<vlen; i++){
				var rd = this.store.getById(this.value[i]);
				if(rd) {
					rawValue[i] = rd.get('name');
				}
			}
			return rawValue;
		}
		return this.value;
	},

	//@Override
	setRawValue: function(v) {
		this.value = v;
	},

	//@Override
	getValue: function () {
		return [].concat(this.value);
	},

	//@Override
	setValue: function (value) {
		this.value = value;
		this.valueStore.reload();
		this.clearInvalid();
	},

	reset: function () {
		if (this.snapshot)
			this.setValue(this.snapshot);
		else
			this.setValue([]);
		this.clearInvalid();
	},
	
	validateValue : function(value){
		if(value.length < 1 || value === this.emptyText) {
			if (this.allowBlank) {
				this.clearInvalid();
			} else {
				this.markInvalid(this.blankText);
				return false;
			}
		}
		if (this.validator) {
			var res = this.validator(value);
			if (res === true) {
				this.clearInvalid();
			} else {
				this.markInvalid(res);
				return false;
			}
		}
		return true;
	},
	
	//add by Junwei, fix qtip error not show
	_recursiveElemQtip: function(el) {
		if(el){
			var e = el.first();
			while (e) {
				e.dom.qtip = el.dom.qtip;
				e.dom.qclass = el.dom.qclass;
				this._recursiveElemQtip(e);
				e = e.next();
			}
		}
	},
	
	markInvalid: function(msg) {
		ns.gui.form.Collection.superclass.markInvalid.call(this,msg);
		if (this.rendered && this.msgTarget=='qtip') {
			this._recursiveElemQtip(this.el);
		}
	},
	
	clearInvalid: function() {
		ns.gui.form.Collection.superclass.clearInvalid.call(this);
		if (this.msgTarget=='qtip') {
			this._recursiveElemQtip(this.el);
		}
	}
});

Ext.reg('collection', ns.gui.form.Collection);

Ext.namespace('ns.gui.form.selector');

ns.gui.form.selector.DataViewSelector = Ext.extend(Ext.form.Field, {
	defaultAutoCreate: {tag: 'div'}, displayField: 'name', valueField: 'id',
	sWidth: 150, sHeight: 320, isFormField: true, hideLabel: true, baseCls: 'form-selector',
	availableTitle: L('Available'), selectedTitle: L('Selected'),
	storeFilter: null,//add storeFilter by Junwei@20091223, filter on seletor
	initComponent: function () {
		this.addEvents('aftermove');
		this.fromStore = new ns.gui.data.ProxyStore({
			autoLoad: true,	//Add by Junwei@20090813: fix create selector after store loaded!
			refStore: this.store
		});
		if (this.storeFilter) {
			this.fromStore.setFilter(this.storeFilter);
		}
		this.toStore = new Ext.data.Store({
			proxy: new Ext.data.MemoryProxy(),
			sortInfo: this.store.sortInfo,
			reader: this.store.reader
		});
		ns.gui.form.selector.DataViewSelector.superclass.initComponent.call(this);
	},
	
	//add by Junwei@20091224, set storeFilter
	setStoreFilter: function(filter) {
		this.storeFilter = filter;
		this.fromStore.setFilter(filter);
	},

	transferRecord: function (r, from, to) {
		if (!(r instanceof Array))
			r = [r];
		for (var i = 0; i < r.length; ++i) {
			if (to.sortInfo) {
				to.addSorted(r[i]);
			} else {
				to.add(r[i]);
			}
			from.remove(r[i]);
		}
	},

	isSelected: function (r) {
		return this.value.indexOf(r.get(this.valueField)) != -1;
	},

	reset: function () {
		this.value = [];
		this.toStore.each(function (r) {
			this.transferRecord(r, this.toStore, this.fromStore);
		}, this);
		this.refresh();
	},

	initValue: function () {
		this.setValue(this.value);
	},

	refresh: function () {
		if (this.fromView) {
			this.fromView.refresh();
			this.toView.refresh();
		}
	},

	setValue: function (value) {
		this.reset();
		if (value) {
			this.value = value;
			this.fromStore.each(function (r) {
				if (this.isSelected(r)) {
					this.transferRecord(r, this.fromStore, this.toStore);
				}
			}, this);
			this.refresh();
		}
	},
	
	getValue: function () {
		return [].concat(this.value);
	},

	setRawValue: function (v) {
		this.value = v;
	},

	getRawValue: function () {
		return this.value;
	},

	onRender: function (ct, position) {
		ns.gui.form.selector.DataViewSelector.superclass.onRender.call(this, ct, position);

		// from view
		var tpl = '<tpl for="."><div class="'+this.baseCls+'-item'+'';
		if (Ext.isIE || Ext.isIE7) {
			tpl += '" unselectable=on';
		} else {
			tpl += ' x-unselectable"';
		}
		tpl += '">{[ns.gui.grid.renderer.hsc(values.'+this.displayField+')]}</div></tpl>';
		this.tpl = new Ext.XTemplate(tpl);
		this.tpl.compile();

		this.fromView = new Ext.DataView({
			multiSelect: true,
			store: this.fromStore,
			tpl: this.tpl,
			itemSelector: 'div.'+this.baseCls+'-item',
			selectedClass: this.baseCls+'-selected',
			listeners: {
				dblclick: function (dw, index, node, e) {
					this.onMove('forward');
				},
				scope: this
			}
		});
		// to view
		this.toView = new Ext.DataView({
			multiSelect: true,
			store: this.toStore,
			tpl: this.tpl,
			itemSelector: 'div.'+this.baseCls+'-item',
			selectedClass: this.baseCls+'-selected',
			listeners: {
				dblclick: function (dw, index, node, e) {
					this.onMove('back');
				},
				scope: this
			}
		});

		
		var p1 = new Ext.form.FieldSet({
			height: this.sHeight, width: this.sWidth, title: this.availableTitle, style: 'background:white;overflow:auto;position:relative;border-top:1px solid #B5B8C8;',
			items:this.fromView,
			autoScroll:true
		});
		var buttons = new Ext.Container({
			style: 'margin-top:'+(this.sHeight/2-20)+'px',
			defaults: { xtype: 'button', scope: this },
			items: [{
					icon:'/images/button/right2.gif',
					handler: function () {
						this.onMove('forward');
					}
				}, {
					icon:'/images/button/left2.gif',
					handler: function () {
						this.onMove('back');
					}
			}]
		});
		var p2 = new Ext.form.FieldSet({
			height: this.sHeight, width: this.sWidth, title: this.selectedTitle,style: 'background:white;overflow:auto;position:relative;border-top:1px solid #B5B8C8;',
			items:this.toView,
			autoScroll:true
		});
		var p = new Ext.Container({
			layout: 'column',style:'padding:5px;',
			items:[p1,buttons,p2]
		});
		p.render(this.el);
	},

	onMove: function (direction,specificRecords) {
		var r, fs, fv, ts, tv,slen;
		if (direction == 'forward') {
			fs = this.fromStore;
			fv = this.fromView;
			ts = this.toStore;
			tv = this.toView;
		} else {
			fs = this.toStore;
			fv = this.toView;
			ts = this.fromStore;
			tv = this.fromView;
		}
		if(specificRecords){
			var r = specificRecords;
		}else{
			var r = fv.getSelectedRecords();
		}
		slen = r.length;
		for (var i = 0; i < slen; ++i) {
			var v = r[i].get(this.valueField);
			if (direction == 'forward') {
				this.value[this.value.length] = v;
			} else {
				this.value.remove(v);
			}
		}
		if (slen) {
			this.transferRecord(r, fs, ts);
			this.fireEvent('aftermove',this,r,fs,ts,direction);
			this.refresh();
		}
	}
});
Ext.reg('ftselector', ns.gui.form.selector.DataViewSelector);

ns.gui.form.selector.Window = Ext.extend(Ext.Window, {
	constrain: true, resizable: false, modal: true, border: false, plain: true,
	width:400, height:400,layout:'anchor',autoHeight:false,// for window grow in ie ,add by xiao li 2010.06.28
	// destroy on close
	closeAction: 'hide', buttonAlign:'right', height: 'auto',
	errInfo: new Ext.Template('<div style = "display: {dis};color:red;text-align:{align};font-size:12px;width:100%;" ><span class = "error-icon">'+L('selected Item is necessary')+'</span></div>'),
	initComponent: function () {
		this.addEvents(['ok', 'cancel']);
		this.items = [this.selector];
		
		// modify by xiao li 09.12.14
		if(!this.errPanel){
			this.errPanel =  this.id+'_err';
			this.items.push({id: this.errPanel,xtype:'container',height:25}); 
		}
		if(!this.errTitle){
			this.errTitle = this.id+'_title';
			this.title = this.title+'<span id="'+this.errTitle+'"></span>';
		}
		this.buttons = [{ text: L('Ok'), handler: this.onOk, scope: this }, 
		                { text: L('Cancel'), handler: this.onCancel, scope: this }];

		ns.gui.form.selector.Window.superclass.initComponent.call(this);
	},

	initEvents: function () {
		ns.gui.form.selector.Window.superclass.initEvents.call(this);

		this.on('hide', this.onHide, this);
	},

	reset: function () {
		this.selector.reset();
	},

	getValue: function () {
		return this.processValue(this.selector.getValue());
	},

	setValue: function (v) {
		this.selector.setValue(this.processValue(v));
	},

	show: function (collection) {
		if (!this.rendered) this.render(Ext.getBody());
		//add by xiao li 09.12.14
		this.errInfo.overwrite(this.errTitle,{dis:'none',align:''});
		this.errInfo.overwrite(this.errPanel,{dis:'none',align:''});
		if (collection) {
			this.collection = collection;
			this.selector.setValue(this.processValue(collection.getValue()));
		}
		ns.gui.form.selector.Window.superclass.show.call(this);
	},

	onOk: function () {
		if (!this.fireEvent('ok', this)) {
			return;
		}
		if (this.collection) {
			// add by xiao li 09.12.14
			var v = this.getValue();
			if(!this.collection.allowBlank && (v == '')) {
				if(this.collection.errTitle){
					this.errInfo.overwrite(this.errTitle,{dis:'block',align:'left'})
				}else{
					this.errInfo.overwrite(this.errPanel,{dis:'block',align:'right'});
				}
				return;
			}else{
				this.collection.clearInvalid();
			}
			this.collection.setValue(v);
			this.hide();
		}
	},

	onCancel: function () {
		this.fireEvent('cancel', this);
		this.hide();
	},

	onHide: function () {
		this.reset();
	},
	
	processValue: function (v) {
		return v;
	}
});

//add NsCollection by Junwei@20091223
ns.gui.form.NsCollection = Ext.extend(ns.gui.form.Collection, {
	selectorTitle: L('Select Object(s)'),
	selectorWidth: 350,//Ext.isIE ? 350 : 'auto',
	storeFilter: null,
	initComponent: function () {
		this.getSelectorWindow();
		ns.gui.form.NsCollection.superclass.initComponent.call(this);
	},
	
	getSelectorWindow: function() {
		if (!this.selectorWindow) {
			// modify by xiao li ,extend the config 
			this.selector = new ns.gui.form.selector.DataViewSelector({
				store: this.store,
				storeFilter: this.storeFilter
			});
			var selectorCfg = {
				width: this.selectorWidth,
				title: this.selectorTitle,
				selector: this.selector,
				listeners: {
					'render':function(){
						this.store.on('beforeload',function(){
							if(!this.wmask){ 
								this.wmask = new Ext.LoadMask(this.selectorWindow.el, { msg: 'ss' }); 
								this.wmask.show();
							}else if(this.selectorWindow.isVisible()){
								this.wmask.show();
							}
						},this);
						this.store.on('load',function(_st){
							if( this.selectorWindow && this.selectorWindow.isVisible()){
								var selector =this.selector, toStore = selector.toStore, selLen = toStore.getCount();
								if (selLen>0){
									var selectedRd = [];
									toStore.each(function(rd){
										selectedRd.push(rd.get('id'));
									});
									toStore.removeAll();
									selector.setValue(selectedRd);
								}
							}
							this.wmask.hide();
						},this);
					 },
					 scope:this
				}
			};
			if(this.selectorConfig){
				Ext.apply(selectorCfg,this.selectorConfig);
			}
			this.selectorWindow = new ns.gui.form.selector.Window(selectorCfg);
		}
		return this.selectorWindow;
	},
	
	setStoreFilter: function(filter) {
		this.storeFilter = filter;
		if(!this.selector) {
			this.getSelectorWindow();
		}
		this.selector.setStoreFilter(filter);
	},
	
	onClick: function () {
		if (this.disabled) return;	//disabled
		ns.gui.form.NsCollection.superclass.onClick.call(this);
		this.getSelectorWindow().show(this);
	}
});
Ext.reg('nscollection', ns.gui.form.NsCollection);
//end add NsCollection

Ext.namespace('ns.gui.ajax');

ns.gui.ajax = {
	request: function (options) {
		if (options.mask)
			options.mask.show();

		options.innerCallback = options.callback;
		options.innerScope = options.scope;

		options.callback = ns.gui.ajax.requestCallback;
		options.scope = ns.gui.ajax;

		Ext.Ajax.request(options);
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
		if (options.mask)
			options.mask.hide();
	}

}
/*
 * add by xiao li 09.12.4 
 * 
 * process ip address
 */
ns.gui.lib.ipLongRange = function(){
	
};
ns.gui.lib.ipLongRange.prototype = {
	/*
	 * get ip range like 1.1.2.2/24
	 * [ip]==1 return ip range else return value range
	 */
	getIpRange: function(num, ip){
		this.explodeIp(num);
		var min = this.getMinIp(),max = this.getMaxIp();
		if(ip == 1){//return 'xxx.xxx.xxx.xxx' 
			return [this.long2ip(min),this.long2ip(max)];
		}else{//return values
			return [min,max];
		}
    },
	ip2long: function (ip) {
		ip = ip.split('.');
		return parseInt(ip[0])*Math.pow(2, 24)+parseInt(ip[1])*Math.pow(2, 16)+parseInt(ip[2])*Math.pow(2, 8)+parseInt(ip[3]);
	},
	long2ip:function ( proper_address ) {
	    // Converts an (IPv4) Internet network address into a string in Internet standard dotted format  
	    // 
	    // version: 909.322
	    // discuss at: http://phpjs.org/functions/long2ip
	    // *     example 1: long2ip( 3221234342 );
	    // *     returns 1: '192.0.34.166'
	    
	    var output = false;    
	    if ( !isNaN( proper_address ) && ( proper_address >= 0 || proper_address <= 4294967295 ) ) {
	        output = Math.floor(proper_address / Math.pow( 256, 3 ) ) + '.' +
	            Math.floor( ( proper_address % Math.pow( 256, 3 ) ) / Math.pow( 256, 2 ) ) + '.' +
	            Math.floor( ( ( proper_address % Math.pow( 256, 3 ) )  % Math.pow( 256, 2 ) ) / Math.pow( 256, 1 ) ) + '.' +
				Math.floor( ( ( ( proper_address % Math.pow( 256, 3 ) ) % Math.pow( 256, 2 ) ) % Math.pow( 256, 1 ) ) / Math.pow( 256, 0 ) );
	    }
	    
	    return output;
	},
	explodeIp: function(ips){
		var iparr = ips.split('/'),
		 	mask = parseInt(iparr[1]),
			ip = this.ip2long(iparr[0]);
		this.ip = ip;
		this.mask =32-mask;
		//return [ip,mask];
	},
	/*
	 * ip2: the base binary ip string. exam:1111111111111000... total number is 32 
	 * f: the flag for getting min or max ,0 is min
	 */
	getMaxIp: function(ips){
		if(!this.min){
			if(!ips){
				return false;
			}
			this.getMinIp(ips);
		}
		var min = this.min,
			mask = this.mask;
		var max = min+Math.pow(2,mask);
		this.max = max;
		return max
	},
	/*
	 * [ips]: 192.168.0.1/20
	 */
	getMinIp: function(ips){
		if(!this.ip){
			if(!ips){
				return false;
			}
			this.explodeIp(ips);
		}
		var ip = this.ip,
			mask = this.mask;
		var ip1 = ip>>mask<<mask;
		if(ip1<0){
			ip1+=4294967296;
			if(ip1>4294967295){
				ip1 = 4294967295;
			}
		}
		this.min = ip1;
		return ip1;
	}
}
/*
 * add by xiao li 09.12.4 
 * when click show the tip information
 * cfg:{
 * 	id:'contentElId', //the id of click text
 * 	info:'the format op ip',// the tip window information
 *  title: 'ip format',//the tip window title
 * }
 * example code:
 * 
 * var batchIp_tip = new ns.gui.lib.clickTip({
 * 		id:'batchIp_tip',
 * 		info:'1.0.0.1<br>192.168.1.0/24<br>192.168.1.1-192.168.1.5',
 * 		title:L('format')
 * 		});
 * var tip = batchIp_tip.getTip();
 * 
 * //if you don't want to show the tip's click text immediately ,don't executive insertText().
 * // executive it when you want the click text show.
 * batchIp_tip.insertText();
 * 
 * // add the tip to anywhere you want
 * var panel = new Ext.Panel({
 * 		items:[tip]
 * });
 * 
 */
ns.gui.lib.clickTip = function(cfg){
	this.txtid = '';// the span id ,exam :<span id = 'txtid'>click text</span>;
	this.config = {
			clickTxtWidth:100,
			clickTxtHeight:18
	};
	this.clickPanelCfg = {};
	if(cfg.clickPanelCfg){
		this.clickPanelCfg = Ext.apply(this.clickPanelCfg,cfg.clickPanelCfg);
	}
	if(cfg) Ext.apply(this.config, cfg); 
};

ns.gui.lib.clickTip.prototype = {
	getTip: function(){
		if(!this.tipPanel){
			this._cre();
			var tipPanelCfg = {
					contentEl: this.config.id
			};
			tipPanelCfg = Ext.apply(tipPanelCfg, this.clickPanelCfg);
			this.tipPanel = new Ext.Panel(tipPanelCfg);
		}
		//Ext.get(this.config.id).show();
		return this.tipPanel;
	},
	
	_show: function(e){
		this.tip.targetXY = e.getXY();
	   	this.tip.show();
	},
	insertText: function(){
		var txtEl = document.getElementById(this.txtid);
		txtEl.innerHTML = '<span style = "color:blue;height:'+this.config.clickTxtHeight+'px;cursor:pointer; font-size:12; text-decoration : underline;width:'+this.config.clickTxtWidth+'px;">'+L('batch format')+'</span>';
	},
	_cre: function(){
		// create div for contentEl
		if(!Ext.get(this.config.id)){
			var tp = document.createElement('div');
			tp.setAttribute('id',this.config.id);
		}else{
			var tp = document.getElementById(this.config.id);
		}
		
		if(Ext.isIE){
			document.body.insertBefore(tp);
		}else{
			Ext.getBody().appendChild(tp);
		}
		// create the 'click text' span 
		var el1 = document.createElement('span');
		this.txtid = this.config.id+'_info';
		el1.setAttribute('id',this.txtid);
		//create the tip target span, the tip position
		var el2 = document.createElement('span');
		var targetid = this.config.id+'_tar';
		el2.setAttribute('id',targetid);
		tp.appendChild(el1);
		tp.appendChild(el2);
		//add click event to the 'click text' span 
		var txtEl = Ext.get(this.config.id+'_info');
		if(txtEl){
			txtEl.on('click',function(e){
				this._show(e);
			},this);
		}
		//create tip window
		var batchFormat = new Ext.ToolTip({
				title: this.config.title?this.config.title:'',
		    	target: targetid,
		        html: this.config.info,
		        autoHide: false,
		        closable: true,
		        draggable:true
		    });
	   this.tip = batchFormat;
	}
}
/*
 * add by xiao li 10.01.15, show the prompt information in alert window;
 */
ns.gui.lib.showPromptWin = function(msg){
	Ext.Msg.show({
		title: L('prompt title'), msg: msg,
		buttons: Ext.Msg.OK, icon: Ext.MessageBox.INFO, minWidth:200 
	});
}

Ext.Panel.prototype.addNsTitleButton = function (configs) {
	if (!this.header)
		return;

	if (!(configs instanceof Array))
		configs = [configs];
	var buttons = {};
	var  ilen = configs.length;
	for (var i = 0, buttons = []; i < ilen; ++i) {
		buttons[configs[i].name || i] = this._addOneNsTitleButton(configs[i]);
	}
	return buttons;
}

Ext.Panel.prototype._addOneNsTitleButton = function(config) {
	var ele = this.header.insertFirst({
		tag: 'div', 
		cls: [].concat('ns_title_button', config.buttonCls).join(' '),
		children: [].concat(
			config.iconCls ? {
				tag: 'img',
				cls: [].concat('ns_title_icon_button', config.iconCls).join(' '),
				src: Ext.BLANK_IMAGE_URL
			} : '',
			{
				tag: 'nsspan', 
				cls: [].concat(config.textCls).join(' '),
				html: config.textHtml || 'button'
			}
		)
	})
	if (config.handler)
		ele.on('click', config.handler, config.scope);
	return ele;
};
