import { Lesson } from "../models/lesson.model";
import * as LessonActions from "../actions";

export interface LessonsState {
    lesson: Lesson,
    isLoaded: boolean
};


const initialState: LessonsState = {
    lesson: null,
    isLoaded: false
};


export function reducer(state: LessonsState = initialState, action: LessonActions.Actions) {
    switch (action.type) {

        case LessonActions.GET_LESSON:
            return {
                ...state,
                isLoaded: false
            };
        case LessonActions.GET_LESSON_SUCCESS:
            return {
                lesson: action.payload,
                isLoaded: true
            };

        case LessonActions.ADD_LESSON:
            return {
                ...state,
                isLoaded: false
            };

        case LessonActions.ADD_LESSON_SUCCESS:
            return {
                lesson: action.payload,
                isLoaded: true
            };

        case LessonActions.UPDATE_LESSON:
            return {
                ...state,
                isLoaded: false
            };

        case LessonActions.UPDATE_LESSON_SUCCESS:
            return {
                lesson: action.payload,
                isLoaded: true
            };

        case LessonActions.DELETE_LESSON:
            return {
                ...state,
                isLoaded: false
            };

        case LessonActions.DELETE_LESSON_SUCCESS:
            return {
                ...state,
                isLoaded: true
            };

        default:
            return state;
    }
}