
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
            _Frame.width = 500;
            _Frame.height = 600;
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
            _Frame.width = 600;
            _Frame.height = 500;
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
            _Frame.width = 600;
            _Frame.height = 500;
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
            _Frame.width = 600;
            _Frame.height = 500;
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
            _Frame.width = 600;
            _Frame.height = 500;
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
    }

    _Frame.src = iframeSrc;/// address of the iframe webpage
    if (applicationName === 'showBiztalk') {
        _Frame.injectableCode = `<div id="${_Frame.id}" class="popup shadow" style="border-radius:8px;position:absolute;top:10px; left:10px;width: ${_Frame.width + 20}px; height: ${_Frame.height + 20}px; display:none; z-index: ${getNewTopFrameZIndex()}; padding:10px"> <nav onclick="changeZIndex('${_Frame.id}')" id="nav_${_Frame.id}" style="border-top-left-radius:8px;border-top-right-radius:8px;height:50px;width: ${_Frame.width}px;" class=""> <a style="position:absolute;top:0px;left:10px" id="title_${_Frame.id}" class="navbar-brand" href="#"><i style="color:black" class="${_Frame.iconClass}"></i> ${_Frame.title}</a> <div style="position:absolute;height:50px;right:10px;top:5px;width:250px"><div style="position:absolute;right:0px"><button style="background-color: rgb(126, 126, 126);"  id="btnFrameMinimize_${_Frame.id}" class="btn btn-sm btn-secondary" onclick="event.preventDefault();minimizeFrame('${_Frame.id}')" ><i class="fa-solid fa-window-minimize"></i></button> <button style="background-color: rgb(126, 126, 126);"  id="btnFrameClose_${_Frame.id}" class="btn btn-sm btn-secondary" onclick="event.preventDefault();closeFrame('${_Frame.id}');"><i class="fa-solid fa-circle-xmark"></i></button></div></nav> <iframe id="iframe_${_Frame.id}" src="" scrolling="no" style="margin-left:0px;width:1000px;height:600px; overflow:hidden"></iframe></div>`;

    }
    else {
        _Frame.injectableCode = `<div id="${_Frame.id}" class="popup shadow" style="border-radius:8px;position:absolute;top:10px; left:10px;width: ${_Frame.width + 20}px; height: ${_Frame.height + 20}px; display:none;z-index: ${getNewTopFrameZIndex()}; padding:10px"> <nav onclick="changeZIndex('${_Frame.id}')" id="nav_${_Frame.id}" style="border-radius:8px;margin-bottom:10px;height:50px;width: ${_Frame.width}px;" class="navbar navbar-dark"> <a style="position:absolute;top:0px;left:10px" id="title_${_Frame.id}" class="navbar-brand" href="#"><i style="color:black" class="${_Frame.iconClass}"></i> ${_Frame.title}</a> <div style="position:absolute;height:50px;right:10px;top:5px;width:250px"><div style="position:absolute;right:0px"><button style="background-color: rgb(126, 126, 126);" id="btnFrameMinimize_${_Frame.id}" class="btn btn-sm btn-secondary" onclick="event.preventDefault();minimizeFrame('${_Frame.id}')" ><i class="fa-solid fa-window-minimize"></i></button>  <button style="background-color: rgb(126, 126, 126);"  id="btnFrameMaximize_${_Frame.id}" class="btn btn-sm btn-secondary" onclick="event.preventDefault();maximizeFrame('${_Frame.id}')"><i class="fa-solid fa-window-maximize" id="iconFrameMaximize_${_Frame.id}"></i></button>  <button style="background-color: rgb(126, 126, 126);"  id="btnFrameClose_${_Frame.id}" class="btn btn-sm btn-secondary" onclick="event.preventDefault();closeFrame('${_Frame.id}');"><i class="fa-solid fa-circle-xmark"></i></button></div></div> </nav> <iframe id="iframe_${_Frame.id}" src="" style="margin-left:10px;width: ${_Frame.width-20}px; height: ${_Frame.height - 60}px;overflow:hidden"></iframe></div>`;

    }

    /// visible|minimized|absent|null
    _Frame.displayState = null;

    return _Frame;
}