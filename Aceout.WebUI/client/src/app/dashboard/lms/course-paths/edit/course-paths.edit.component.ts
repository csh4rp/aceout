import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ShowOnDirtyErrorStateMatcher } from '@angular/material';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import { getCoursePath } from 'src/app/store/dashboard/lms/course-paths/selectors/course-paths.selectors';
import { CoursePath } from 'src/app/store/dashboard/lms/course-paths/models/course-path.model';
import * as CoursePathActions from 'src/app/store/dashboard/lms/course-paths/actions';
import { FormAssigner } from 'src/app/helpers/formAssigner';
import { assignFormValues } from 'src/app/helpers/forms';

@Component({
  templateUrl: './course-paths.edit.component.html'
})
export class CoursePathsEditComponent implements OnInit {

  dataForm: FormGroup;
  coursePath$: Observable<CoursePath>;
  submitText: string;
  titleText: string;
  private id: number;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private store: Store<AppState>) {

  }

  get form() {
    return this.dataForm.controls;
  }

  ngOnInit() {
    this.dataForm = new FormGroup({
      name: new FormControl('', Validators.required),
      description: new FormControl('', [])
    });

    this.route.params.subscribe(p => {
      this.id = parseInt(p['id']);

      if (this.id > 0) {
        this.submitText = 'Save changes';
        this.titleText = 'Edit course path';

        this.coursePath$ = this.store.select(getCoursePath);
        this.coursePath$.subscribe(coursePath => {
          if(coursePath){
            assignFormValues(coursePath, this.dataForm);
          }
        });

        this.store.dispatch(new CoursePathActions.GetCoursePath(this.id));
      }
      else{
        this.submitText = 'Add';
        this.titleText = 'Add course path'
      }
    });
  }

  getCoursePath() : CoursePath{
    const coursePath = <CoursePath>this.dataForm.value;
    coursePath.id = this.id;

    return coursePath;
  }

  onSubmit() {
    const coursPath = this.getCoursePath();

    if(this.id === 0){
      this.store.dispatch(new CoursePathActions.AddCoursePath(coursPath));
    }
    else{
      this.store.dispatch(new CoursePathActions.UpdateCoursePath(coursPath));
    }
  }

  back() {
    this.router.navigate(['dashboard', 'lms', 'course-paths']);
  }

}

