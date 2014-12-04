EasyLanguage = function (key) {
	var json = EasyLanguage.JSON || {};
	return translateString(json,key);
}
function translateString(json,key){
	if (typeof key == 'string') {
		if (typeof json[key] != 'undefined') {
			return json[key];
		} else {
			return key;
		}
	} else {
		if (key==null) return '';
		var raw = key[0];
		if (typeof json[raw] != 'undefined') {
			raw = json[raw];
		}

		var re = /{([^{}]+)}/g, vc = key[1];
		return raw.replace(re, function (substr, k, offset, original) {
			var v = vc[k];
			if (v instanceof Array) {
				return L(v);
			} else {
				return v;
			}
		});
	}
}
L = EasyLanguage;
MonitorLanguage = function (key) {
	if (typeof MonitorLanguage.JSON == 'undefined') {
		return 'no string';
	}
	var json = MonitorLanguage.JSON || {};
	return translateString(json,key);
}
M = MonitorLanguage;

bgManagerLanguage = function (key) {
	if (typeof bgManagerLanguage.JSON == 'undefined') {
		return 'no string';
	}
	var json = bgManagerLanguage.JSON || {};
	return translateString(json,key);
}
BG = bgManagerLanguage;


FrameLanguage = function (key) {
	if (typeof FrameLanguage.JSON == 'undefined') {
		return 'no string';
	}
	var json = FrameLanguage.JSON || {};
	return translateString(json,key);
}
F = FrameLanguage;