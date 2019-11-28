import { CourseDetails } from "../models/course-details.model";
import * as CourseActions from "../actions";
import { CourseViewModel } from "../models/course.view-model";

export interface CoursesState {
    course: CourseViewModel,
    isLoaded: boolean
};

export const initialState: CoursesState = {
    course: null,
    isLoaded: false
};

export function reducer(state: CoursesState = initialState, action: CourseActions.Actions) {
    switch (action.type) {

        case CourseActions.GET_COURSE:
            return {
                ...state,
                isLoaded: false
            };

        case CourseActions.GET_COURSE_SUCCESS:
            return {
                course: action.payload,
                isLoaded: true
            };

        case CourseActions.ADD_COURSE:
            return {
                ...state,
                isLoaded: false
            };

        case CourseActions.ADD_COURSE_SUCCESS:
            return {
                ...state,
                isLoaded: true
            };

        case CourseActions.UPDATE_COURSE:
            return {
                ...state,
                isLoaded: false
            };

        case CourseActions.UPDATE_COURSE_SUCCESS:
            return {
                ...state,
                isLoaded: true
            };

        case CourseActions.DELETE_COURSE:
            return {
                ...state,
                isLoaded: false
            };

        case CourseActions.DELETE_COURSE_SUCCESS:
            return {
                ...state,
                isLoaded: true
            };

        default:
            return state;
    }
}