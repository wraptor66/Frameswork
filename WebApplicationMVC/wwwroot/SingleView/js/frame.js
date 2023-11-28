
//key to this object is the source_page
//the url is the key to the app
//the millisecond is the key to the instance
//pass in a json object
//parameters--
//ApplicationName -> string
//DialogTitle -> string
//BiztalkId -> int
//Epoint -> int
//DialogName -> string
//BaseModel -> string

//*** query string variables need to be all lowercase
let Frame = function (FrameObject) {
    let _Frame = {};
    _Frame.id = 'Frame_' + new Date().getTime().toString();
    /// add a switch statement for mapping the applicationName to the iframeAddress
    let applicationName = FrameObject.applicationName;
    let iframeSrc = '';
    let iconClass = 'fa-duotone fa-users-viewfinder';
    switch (applicationName) {
        case 'login': {
            iframeSrc = `componentpages/login.html`;
            _Frame.autoOpen = false;
            _Frame.width = window.innerWidth;
            _Frame.height = window.innerHeight;
            _Frame.title = FrameObject.title;
            _Frame.resizable = true;
            _Frame.modal = false;
            _Frame.restoredHeight = 0;
            _Frame.restoredWidth = 0;
            _Frame.restoredTop = 0;
            _Frame.restoredLeft = 0;
            _Frame.iconClass = 'fa-duotone fa-file-user';
            break;
        }
        case 'step_1-0': {
            iframeSrc = `componentpages/workflow/step_1-0.html`;
            _Frame.autoOpen = false;
            _Frame.width = window.innerWidth;
            _Frame.height = window.innerHeight;
            _Frame.title = FrameObject.title;
            _Frame.resizable = true;
            _Frame.modal = false;
            _Frame.restoredHeight = 0;
            _Frame.restoredWidth = 0;
            _Frame.restoredTop = 0;
            _Frame.restoredLeft = 0;
            _Frame.iconClass = 'fa-regular fa-layer-plus';
            break;
        }
        case 'step_1-1': {
            iframeSrc = `componentpages/workflow/step_1-1.html?inputfield1=${FrameObject.inputfield1}&inputfield2=${FrameObject.inputfield2}`;
            _Frame.autoOpen = false;
            _Frame.width = window.innerWidth;
            _Frame.height = window.innerHeight;
            _Frame.title = FrameObject.title;
            _Frame.resizable = true;
            _Frame.modal = false;
            _Frame.restoredHeight = 0;
            _Frame.restoredWidth = 0;
            _Frame.restoredTop = 0;
            _Frame.restoredLeft = 0;
            _Frame.iconClass = 'fa-regular fa-layer-plus';
            break;
        }
        case 'step_1-2': {
            iframeSrc = `componentpages/workflow/step_1-2.html`;
            _Frame.autoOpen = false;
            _Frame.width = window.innerWidth;
            _Frame.height = window.innerHeight;
            _Frame.title = FrameObject.title;
            _Frame.resizable = true;
            _Frame.modal = false;
            _Frame.restoredHeight = 0;
            _Frame.restoredWidth = 0;
            _Frame.restoredTop = 0;
            _Frame.restoredLeft = 0;
            _Frame.iconClass = 'fa-regular fa-layer-plus';
            break;
        }
        case 'LoginCoding': {
            iframeSrc = `componentpages/coding/login.html`;
            _Frame.autoOpen = false;
            _Frame.width = window.innerWidth;
            _Frame.height = window.innerHeight;
            _Frame.title = FrameObject.title;
            _Frame.resizable = true;
            _Frame.modal = false;
            _Frame.restoredHeight = 0;
            _Frame.restoredWidth = 0;
            _Frame.restoredTop = 0;
            _Frame.restoredLeft = 0;
            _Frame.iconClass = 'fa-sharp fa-solid fa-circle-info';
            break;
        }
        case 'DetailsForm': {
            iframeSrc = `componentpages/DetailsForm/DetailsForm.html`;
            _Frame.autoOpen = false;
            _Frame.width = window.innerWidth;
            _Frame.height = window.innerHeight;
            _Frame.title = FrameObject.title;
            _Frame.resizable = true;
            _Frame.modal = false;
            _Frame.restoredHeight = 0;
            _Frame.restoredWidth = 0;
            _Frame.restoredTop = 0;
            _Frame.restoredLeft = 0;
            _Frame.iconClass = 'fa-sharp fa-solid fa-edit';
            break;
        }
        case 'ShowDetails': {
            iframeSrc = `componentpages/DetailsForm/ShowDetails.html`;
            _Frame.autoOpen = false;
            _Frame.width = window.innerWidth;
            _Frame.height = window.innerHeight;
            _Frame.title = FrameObject.title;
            _Frame.resizable = true;
            _Frame.modal = false;
            _Frame.restoredHeight = 0;
            _Frame.restoredWidth = 0;
            _Frame.restoredTop = 0;
            _Frame.restoredLeft = 0;
            _Frame.iconClass = 'fa-sharp fa-solid fa-eye';
            break;
        }
    }


    _Frame.src = iframeSrc;/// address of the iframe webpage
    if (FrameObject.help != undefined && FrameObject.help) {
        _Frame.injectableCode = `<div id="${_Frame.id}" class="popup" style="border-radius:0px;position:absolute;top:0px; left:0px;width: ${_Frame.width - 0}px; height: ${_Frame.height}px; display:none;z-index: ${getNewTopFrameZIndex()}; padding:10px"> <div id="nav_${_Frame.id}" style="border-radius:8px;margin-bottom:10px;height:40px;width: ${_Frame.width - 20}px;" class="_ui-dialog-title"> <span style="color:white;float:left;margin-top:2px;margin-left:5px;height:30px" id="title_${_Frame.id}"><i style="color:white" class="${_Frame.iconClass}"></i> ${_Frame.title}</span> <div style="position:absolute;height:30px;right:15px;margin-top:0px;width:250px"><div style="float:right;margin-right:0px;margin-top:0px"><button style="background-color: dodgerblue;height:30px"  id="btnFrameClose_${_Frame.id}" class="btn btn-sm btn-secondary" onclick="event.preventDefault();closeFrame('${_Frame.id}')"><i class="fa-regular fa-rectangle-xmark" style="color:white" id="iconFrameClose_${_Frame.id}"></i></button></div></div> </div> <iframe id="iframe_${_Frame.id}" scrolling="true" src="${iframeSrc}" style="margin-left:10px;width: ${_Frame.width - 40}px; height: ${_Frame.height - 80}px;"></iframe></div>`;
    }
    else if (applicationName == 'registerUser' || applicationName == 'login') {
        _Frame.injectableCode = `<div id="${_Frame.id}" class="popup" style="border-radius:0px;position:absolute;top:0px; left:0px;width: ${_Frame.width - 0}px; height: ${_Frame.height}px; display:none;z-index: ${getNewTopFrameZIndex()}; padding:10px"> <div id="nav_${_Frame.id}" style="border-radius:8px;margin-bottom:10px;height:40px;width: ${_Frame.width - 20}px;" class="_ui-dialog-title"> <span style="color:white;float:left;margin-top:2px;margin-left:5px;height:30px" id="title_${_Frame.id}"><i style="color:white" class="${_Frame.iconClass}"></i> ${_Frame.title}</span> <div style="position:absolute;height:30px;right:15px;margin-top:0px;width:250px"><div style="float:right;margin-right:0px;margin-top:0px"><button style="display:none;background-color: dodgerblue;height:30px"  id="btnFrameClose_${_Frame.id}" class="btn btn-sm btn-secondary" onclick="event.preventDefault();closeFrame('${_Frame.id}')"><i class="fa-solid fa-backward-step" style="color:white" id="iconFrameClose_${_Frame.id}"></i></button><button style="display:none;background-color: dodgerblue;height:30px" style="color:white" id="btnFramesClose_${_Frame.id}" class="btn btn-sm btn-secondary" onclick="event.preventDefault();closeFrames('${_Frame.id}');"><i class="fa-duotone fa-house-blank" style="color:white"></i></button></div></div> </div> <iframe id="iframe_${_Frame.id}" scrolling="true" src="${iframeSrc}" style="margin-left:10px;width: ${_Frame.width - 40}px; height: ${_Frame.height - 80}px;"></iframe></div>`;
    }
    else {
        _Frame.injectableCode = `<div id="${_Frame.id}" class="popup" style="border-radius:0px;position:absolute;top:0px; left:0px;width: ${_Frame.width - 0}px; height: ${_Frame.height}px; display:none;z-index: ${getNewTopFrameZIndex()}; padding:10px"> <div id="nav_${_Frame.id}" style="border-radius:8px;margin-bottom:10px;height:40px;width: ${_Frame.width - 20}px;" class="_ui-dialog-title"> <span style="color:white;float:left;margin-top:2px;margin-left:5px;height:30px" id="title_${_Frame.id}"><i style="color:white" class="${_Frame.iconClass}"></i> ${_Frame.title}</span> <div style="position:absolute;height:30px;right:15px;margin-top:0px;width:250px"><div style="float:right;margin-right:0px;margin-top:0px"><button style="background-color: dodgerblue;height:30px"  id="btnFrameClose_${_Frame.id}" class="btn btn-sm btn-secondary" onclick="event.preventDefault();closeFrame('${_Frame.id}')"><i class="fa-solid fa-backward-step" style="color:white" id="iconFrameClose_${_Frame.id}"></i></button><button style="background-color: dodgerblue;height:30px" style="color:white" id="btnFramesClose_${_Frame.id}" class="btn btn-sm btn-secondary" onclick="event.preventDefault();closeFrames('${_Frame.id}');"><i class="fa-duotone fa-house-blank" style="color:white"></i></button></div></div> </div> <iframe id="iframe_${_Frame.id}" scrolling="true" src="${iframeSrc}" style="margin-left:10px;width: ${_Frame.width - 40}px; height: ${_Frame.height - 80}px;"></iframe></div>`;
    }

    /// visible|minimized|absent|null
    _Frame.displayState = null;

    return _Frame;
}                             