/// <reference name="MicrosoftAjax.js"/>
//地区选择对象字典
var SelectAreaObjs = new Object();
//地区选择对象
var SelectAreaObj = function(objId, isAddSpace, spaceText, rootAreaID, datas) {
    
    this.objId = objId;
    this.isAddSpace = isAddSpace;
    this.spaceText = spaceText;
    this.rootAreaID = rootAreaID;
    this.areaDatas = datas;
    this.areaSels = new Object();
    this.hidId = "_hidden" + objId;
    this._init();
   
}

SelectAreaObj.prototype = {
    _init: function() {
        this._bindSelectArea(this.rootAreaID);
        var val = this.getValue();
        if (val != "-1") {
            this.selectAreaId(val);
        }
    },
    _getAreaDataByID: function(entity, id) {
        if (entity.AreaID == id)
            return entity;
        for (var i = 0; i < entity.Sons.length; i++) {
            var son = entity.Sons[i];
            var t = this._getAreaDataByID(son, id);
            if (t != -1)
                return t;
        }
        return -1;
    },
    setValue: function(val) {
        $("#" + this.hidId).val(val);
    },
    getValue: function() {
        return $("#" + this.hidId).val();
    },
    _bindSelectArea: function(areaId) {
        if (this.areaSels[areaId] == null) {
            var entity = this._getAreaDataByID(this.areaDatas, areaId);
            if (entity.Sons.length > 0) {
                var name = this._getAreaSelName(areaId);
                var s = "<select id='" + name + "' />";
                $("#" + this.objId).append(s);
                var sel = $("#" + name);
                if (areaId != this.rootAreaID) {
                    sel.addOption(this.spaceText, areaId);
                }
                else {
                    if (this.isAddSpace) {
                        sel.addOption(this.spaceText, "-1");
                    }
                }
                for (var i = 0; i < entity.Sons.length; i++) {
                    var son = entity.Sons[i];
                    sel.addOption(son.AreaName, son.AreaID);
                }
                var fn = Function.createDelegate(this, function() {
                    var val = sel.getSelectedValue();
                    if (val != "-1") {
                        this.selectAreaId(val);
                    }
                    else {
                        this.__unbindSelectAreaSons(areaId);
                    }
                    this.setValue(val);
                });
                sel.change(fn);
                this.areaSels[areaId] = sel;
            }
        }
    },
    _unbindSelectArea: function(areaId) {
        if (this.areaSels[areaId] != null) {
            var entity = this._getAreaDataByID(this.areaDatas, areaId);
            this.__unbindSelectArea(entity);
        }
    },
    __unbindSelectArea: function(entity) {
        for (var i = 0; i < entity.Sons.length; i++) {
            var son = entity.Sons[i];
            this.__unbindSelectArea(son);
        }
        var sel = this.areaSels[entity.AreaID];
        if (sel != null) {
            this.areaSels[entity.AreaID] = null;
            sel.remove();
        }
    },
    _getAreaSelName: function(areaId) {
        return "_sel" + areaId;
    },
    __unbindSelectAreaSons: function(areaId) {
        var entity = this._getAreaDataByID(this.areaDatas, areaId);
        for (var i = 0; i < entity.Sons.length; i++) {
            var son = entity.Sons[i];
            this._unbindSelectArea(son.AreaID);
        }
    },
    selectAreaId: function(areaId) {
        if(areaId == "")
        {
            return;
        }
        if (this.areaSels[areaId] == null) {
            var entity = this._getAreaDataByID(this.areaDatas, areaId);
            var parentId = entity.ParentID;
            while (this.areaSels[entity.ParentID] == null) {
                entity = this._getAreaDataByID(this.areaDatas, entity.ParentID);
            }
            this.__unbindSelectAreaSons(entity.ParentID);
            var areaIds = this._getBindAreaIDs(areaId, entity.ParentID);
            for (var i = 0; i < areaIds.length; i++) {
                this._bindSelectArea(areaIds[i]);
            }
            this._setBindAreaSelsByAreaId(areaId);
        }
        else {
            this.__unbindSelectAreaSons(areaId);
            var entity = this._getAreaDataByID(this.areaDatas, areaId);
            var sel = this.areaSels[entity.ParentID];
            sel.setSelectedValue(areaId);
        }
    },
    _getBindAreaIDs: function(areaId, endAreaId) {
        entity = this._getAreaDataByID(this.areaDatas, areaId);
        var str = areaId;
        while (entity.ParentID != endAreaId && entity != -1) {
            str = entity.ParentID + "," + str;
            entity = this._getAreaDataByID(this.areaDatas, entity.ParentID);
        }
        return str.split(',');
    },
    _setBindAreaSelsByAreaId: function(areaId) {
        var arr = this._getBindAreaIDs(areaId, this.rootAreaID);
        for (var i = 0; i < arr.length; i++) {
            var entity = this._getAreaDataByID(this.areaDatas, arr[i]);
            var sel = this.areaSels[entity.ParentID];
            sel.setSelectedValue(entity.AreaID);
        }
    }
}

