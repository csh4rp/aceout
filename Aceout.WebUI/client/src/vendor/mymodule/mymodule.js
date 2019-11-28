export class MyModule{
    init(){
        $('<div id="elfinder"/>').elfinder({
            url: '/aceout/file-system/connector',
           getFileCallback: function (file, fm) {
              
              $('#elfinder').dialog('close');
            }
          }).dialog({
            width: 1500
          });
    }
}