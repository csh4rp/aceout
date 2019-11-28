// https://github.com/Studio-42/elFinder/wiki/Integration-with-TinyMCE-4.x
function elFinderBrowser(callback, value, meta) {
  tinymce.activeEditor.windowManager.open({
    file: 'http://localhost/aceout/FileSystem/browse',
    title: 'File Manager',
    width: 900,
    height: 450,
    resizable: 'yes'
  }, {
      oninsert: function (file, fm) {
        var url, reg, info;

        // URL normalization
        url = fm.convAbsUrl(file.url);

        // Make file info
        info = file.name + ' (' + fm.formatSize(file.size) + ')';

        // Provide file and text for the link dialog
        if (meta.filetype == 'file') {
          callback(url, { text: info, title: info });
        }

        // Provide image and alt text for the image dialog
        if (meta.filetype == 'image') {
          callback(url, { alt: info });
        }

        // Provide alternative source and posted for the media dialog
        if (meta.filetype == 'media') {
          callback(url);
        }
      }
    });
  return false;
}

window.onload = function () {
    tinymce.init({
        selector: "textarea",  height: 300,
        plugins:[
            "image"
        ],
       // plugins: [
       //         "advlist autolink link image lists charmap print preview hr anchor pagebreak",
       //         "searchreplace wordcount visualblocks visualchars insertdatetime media nonbreaking",
       //         "table contextmenu directionality emoticons paste textcolor"
       // ],
       // toolbar1: "undo redo | bold italic underline | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | styleselect",
       // toolbar2: "| link unlink anchor | image media | forecolor backcolor  | print preview code ",
        image_advtab: true,
        file_picker_callback: elFinderBrowser
    });




  (function () {
    var myCommands = elFinder.prototype._options.commands;

    var disabled = ['extract', 'archive', 'resize', 'help', 'select']; // Not yet implemented commands in ElFinder.Net

    $.each(disabled, function (i, cmd) {
      (idx = $.inArray(cmd, myCommands)) !== -1 && myCommands.splice(idx, 1);
    });

    var options = {
      url: '/aceout/file-system/connector', // Default (Local File System)
      //url: '/el-finder/azure-storage/connector', // Microsoft Azure Connector
      rememberLastDir: false, // Prevent elFinder saving in the Browser LocalStorage the last visited directory
      commands: myCommands,
      //lang: 'pt_BR', // elFinder supports UI and messages localization. Check the folder Content\elfinder\js\i18n for all available languages. Be sure to include the corresponding .js file(s) in the JavaScript bundle.
      uiOptions: { // UI buttons available to the user
        toolbar: [
          ['back', 'forward'],
          ['reload'],
          ['home', 'up'],
          ['mkdir', 'mkfile', 'upload'],
          ['open', 'download'],
          ['undo', 'redo'],
          ['info'],
          ['quicklook'],
          ['copy', 'cut', 'paste'],
          ['rm'],
          ['duplicate', 'rename', 'edit'],
          ['selectall', 'selectnone', 'selectinvert'],
          ['view', 'sort']
        ]
      },
      getFileCallback: function (file, fm) { // editor callback (see: https://github.com/Studio-42/elFinder/wiki/Integration-with-TinyMCE-4.x)
        // pass selected file data to TinyMCE
        parent.tinymce.activeEditor.windowManager.getParams().oninsert(file, fm);
        // close popup window
        parent.tinymce.activeEditor.windowManager.close();
      }
    };
    $('#elfinder').elfinder(options).elfinder('instance');
  })();


};
