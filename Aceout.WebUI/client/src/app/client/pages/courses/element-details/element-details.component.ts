import { Component, OnInit, ViewChild, ComponentFactoryResolver } from "@angular/core";
import { Store } from "@ngrx/store";
import { AppState } from "src/app/store/app.state";
import { Observable } from "rxjs";
import { UserLessonDetails } from "src/app/store/client/user-lessons/models/user-lesson-details.model";
import * as UserElementSelectors from 'src/app/store/client/user-elements/selectors/user-elements.selectors';
import * as UserElementsActions from 'src/app/store/client/user-elements/actions/user-elements.actions';
import * as UserLessonsActions from 'src/app/store/client/user-lessons/actions/user-lessons.actions';
import { ActivatedRoute, Router } from "@angular/router";
import { UserElement } from "src/app/store/client/user-elements/models/user-element.model";
import { ElementControl } from "../templates/element.control";
import { ElementTemplate } from "../templates/element-template.directive";
import { MaterialType } from "src/app/store/dashboard/lms/materials/models/material-type.model";
import { SingleAnswerComponent } from "../templates/single-answer/single-answer.component";

@Component({
    templateUrl: './element-details.component.html'
})
export class ElementDetailsComponent implements OnInit {

    private position: number;
    private lessonId: number;
    private mode: string;
    private elementControl: ElementControl;

    element$: Observable<UserElement>;
    hasNext$: Observable<boolean>;
    hasPrevious$: Observable<boolean>;
    isElementSaved$: Observable<boolean>;

    get isPreviewMode(): boolean {
        return this.mode === 'preview';
    }

    @ViewChild(ElementTemplate)
    host: ElementTemplate;

    constructor(private store: Store<AppState>,
        private route: ActivatedRoute,
        private factory: ComponentFactoryResolver,
        private router: Router) {

    }

    ngOnInit() {
        this.route.params.subscribe(p => {
            this.position = parseInt(p['position']);
            this.lessonId = parseInt(p['lessonId']);
            this.store.dispatch(new UserElementsActions.GetElements(this.lessonId));
        });

        this.route.queryParams.subscribe(q => {
            this.mode = q['mode'];
        });

        this.element$ = this.store.select(UserElementSelectors.getElement);
        this.element$.subscribe(e => {
            if(e){
                this.typeChanged(e);
            }
        });

        this.hasNext$ = this.store.select(UserElementSelectors.getHasNext);
        this.hasPrevious$ = this.store.select(UserElementSelectors.getHasPrevious);
    }

    navigateNext(){
        this.store.dispatch(new UserElementsActions.SaveElement(this.elementControl.getUserAnswes()));
        this.store.dispatch(new UserElementsActions.NavigateNext( this.position));
    }

    navigatePrevious(){
        this.store.dispatch(new UserElementsActions.SaveElement(this.elementControl.getUserAnswes()));
        this.store.dispatch(new UserElementsActions.NavigatePrevious(this.position));
    }

    finish(){

        this.isElementSaved$ = this.store.select(UserElementSelectors.getIsSaved);
        this.isElementSaved$.subscribe(isSaved => {
            if(isSaved){
                this.store.dispatch(new UserLessonsActions.FinishLesson(this.lessonId));
            }
        });

        this.store.dispatch(new UserElementsActions.SaveElement(this.elementControl.getUserAnswes()));
    }

    typeChanged(element: UserElement) {
        switch (element.materialType) {
          case MaterialType.SingleAnswer:
            this.changeComponent(SingleAnswerComponent, element);
            break;

          default:
            this.host.viewContainerRef.clear();
            break;
        }
      }

      private changeComponent(component: any, element: UserElement) {

        const componentFactory = this.factory.resolveComponentFactory(component);
        const viewContainerRef = this.host.viewContainerRef;
        viewContainerRef.clear();

        const componentRef = viewContainerRef.createComponent(componentFactory);
        this.elementControl = componentRef.instance as ElementControl;
        this.elementControl.dataLoad(element, this.isPreviewMode);
      }

      return(){
          this.router.navigate(['../'], {relativeTo: this.route});
      }

}
