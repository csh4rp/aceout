import * as LessonActions from '../actions';
import { UserLessonDetails } from '../models/user-lesson-details.model';


export interface UserLessonState{
    lesson: UserLessonDetails;
    isLoaded: boolean;
    isCompleted: boolean;
    isStarted: boolean;
}


const initialState: UserLessonState = {
    lesson: null,
    isLoaded: false,
    isCompleted: false,
    isStarted: false
};

export function reducer(state: UserLessonState = initialState, action: LessonActions.Actions){
    switch(action.type){
        
        case LessonActions.GET_LESSON:
            return {
                ...state,
                isLoaded: false
            };

        case LessonActions.GET_LESSON_SUCCESS:
            return {
                lesson: action.payload,
                isLoaded: true,
                isStarted: action.payload.startedDate != null,
                isCompleted: action.payload.completedDate != null
            };

        case LessonActions.START_LESSON:
            return {
                ...state
            };
        
        case LessonActions.START_LESSON_SUCCESS:
            return {
                ...state,
                isStarted: true,
            };
        
        case LessonActions.FINISH_LESSON:
            return {
                ...state
            };


        case  LessonActions.FINISH_LESSON_SUCCESS:
            return {
                ...state,
                isCompleted: true
            }

        default:
            return state;
    }
}
