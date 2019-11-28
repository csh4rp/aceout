import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { UrlHelper } from 'src/app/app.urls';
import { ButtonRowRenderer } from 'src/app/shared-dashboard/grid/buttons/button-row-renderer.component';
import { Router } from '@angular/router';
import { GridControl } from 'src/app/controls/gridControl';
import { extend } from 'webdriver-js-extender';
import { MaterialControl } from '../material.control';
import { AnswerData } from 'src/app/model/dashboard/lms/answer-data';
import { MaterialData } from 'src/app/model/dashboard/lms/material-data';
import { CheckboxRowRenderer } from 'src/app/shared-dashboard/grid/checkbox/checkbox-row-renderer.component';
import { SingleAnswerModel } from './model/single-answer.model';
import { Observable, BehaviorSubject } from 'rxjs';


@Component({
  templateUrl: './single-answer.component.html'
})
export class SingleAnswerComponent extends GridControl implements MaterialControl, OnInit {

  private gridApi: any;
  private gridColumnApi: any;
  private rowData: any[];
  private errorSubject: BehaviorSubject<string[]> = new BehaviorSubject(['']);
  private isControlValid: boolean;

  buttons: any[];
  columns: any[];
  frameworkComponents: any;
  currentName: string;
  errorMessage: string;

  get errors$(): Observable<string[]> {
    return this.errorSubject;
  }

  get isValid(): boolean {
    return this.isControlValid;
  }

  constructor(translate: TranslateService) {
    super(translate);
  }

  ngOnInit() {

    this.buttons = [
      {
        name: 'delete',
        action: p => {
          this.gridApi.forEachNode((node, index) => {
            const item = node.data as SingleAnswerModel;
            if (item.id == p) {
              this.gridApi.updateRowData({ remove: [item] });
            }
          });
        },
        icon: 'trash',
        data: 'id'
      }
    ];

    this.columns = [
      { colId: 'Content', headerName: 'Content', field: 'content', editable: true },
      {
        colId: 'Is correct', cellRenderer: 'checkboxRowRenderer', headerName: 'is correct', field: 'id',
        suppressSizeToFit: true,
        cellRendererParams: { dataKey: 'id', checkedProp: 'isValid' }
      },
      {
        colId: 'button', cellRenderer: 'buttonRowRenderer', headerName: '', field: 'id',
        width: 60, suppressSizeToFit: true,
        cellRendererParams: { buttons: this.buttons }
      }
    ];

    this.frameworkComponents = {
      buttonRowRenderer: ButtonRowRenderer,
      checkboxRowRenderer: CheckboxRowRenderer
    };

  }


  getDataModels(): MaterialData[] {
    const rows = [];

    this.gridApi.forEachNode((node, index) => {
      const item = node.data as SingleAnswerModel;
      const materialData = new MaterialData();
      materialData.id = item.id;
      materialData.content = item.content;
      rows.push(materialData);
    });
    return rows;
  }

  getAnswerModels(): AnswerData[] {
    let answer: SingleAnswerModel;

    this.gridApi.forEachNode((node, index) => {
      const item = node.data as SingleAnswerModel;
      if (item.isValid) {
        answer = item;
        return;
      }
    });

    const answerData = new AnswerData();
    answerData.id = answer.id;

    return [answerData];
  }

  setModels(dataModels: MaterialData[], answerModels: AnswerData[]) {
    this.rowData = [];
    const answerId = answerModels[0].id;

    for (let model of dataModels) {
      const answerModel = new SingleAnswerModel(model.id, model.content);
      answerModel.isValid = model.id == answerId;
      this.rowData.push(answerModel);
    }
  }

  getRowNodeId(param: any) {
    console.log(param);
    if (!param) return undefined;
    return param.id;
  }

  cellValueChanged(value: any) {
    let correctSelected = false;
    let isCurrentValid = true;
    let errorMsg = '';

    this.gridApi.forEachNode((node) => {
      const data = node.data as SingleAnswerModel;
      if (data.isValid && !correctSelected) {
        correctSelected = true;
        errorMsg = '';
      }
      else if (data.isValid && correctSelected) {
        errorMsg = 'Correct answer is already selected';
        isCurrentValid = false;
      }
      else {
        isCurrentValid = false;
      }

    });

    this.isControlValid = isCurrentValid;
    this.errorMessage = errorMsg;
  }

  gridReady(params) {
    this.gridApi = params.api;
    super.onGridReady(params, this.columns);
    this.gridColumnApi = params.columnApi;
    this.gridApi.setRowData(this.rowData);
  }

  addAnswer() {

    if (!this.currentName) {
      this.errorMessage = 'You must specify answer content';
      return;
    }
    else {
      this.errorMessage = '';
    }

    let maxId = 0;

    this.gridApi.forEachNode((node, index) => {
      const item = node.data as SingleAnswerModel;
      if (item.id >= maxId) {
        maxId = item.id + 1;
      }
    });

    const entry = new SingleAnswerModel(maxId, this.currentName);
    this.currentName = '';

    this.gridApi.updateRowData({ add: [entry] });
  }

}

