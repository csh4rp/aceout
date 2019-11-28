import { Component, OnInit, ViewEncapsulation, ViewChild, Output, EventEmitter, Inject, Input } from '@angular/core';
import { FormControl } from '@angular/forms';

declare var $: any;

@Component({
  templateUrl: './file-picker.component.html',
  selector: 'file-picker'
})
export class FilePickerComponent {

  @Input('control')
  control: FormControl;

  @Input('name')
  name: string;

  @Input('placeholder')
  placeholder: string;

  constructor() {
  }

  onChange(val) {
    if (this.control) {
      this.control.setValue($('#' + this.name).val());
    }
  }

  openFilePicker() {
    $('<div id="elfinder"/>').elfinder({
      url: 'http://localhost/aceout/file-system/connector',
      customData: {
        inputName: this.name
      },
      getFileCallback: function (file, fm) {
        $('#' + fm.customData.inputName).val(file.path);
        document.getElementById(fm.customData.inputName).dispatchEvent(new Event('change'));
        $('#elfinder').remove();
      }
    }).dialog({
      width: 1500
    });
  }
}

