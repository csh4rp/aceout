import { CoursePath } from "../models/course-path.model";
import * as CoursePathActions from "../actions";
import { CoursePathDetails } from "../models/course-path-details.model";

export interface CoursePathState {
    coursePath: CoursePath,
    paths: CoursePath[],
    isLoaded: boolean,
    userPaths: CoursePathDetails[],
};

export const initialState: CoursePathState = {
    coursePath: null,
    paths: null,
    isLoaded: false,
    userPaths: null,
};

export function reducer(state: CoursePathState = initialState, action: CoursePathActions.Actions) {
    switch (action.type) {

        case CoursePathActions.GET_COURE_PATH:
            return {
                ...state,
                isLoaded: false
            };

        case CoursePathActions.GET_COURSE_PATH_SUCCESS:
            return {
                ...state,
                coursePath: action.payload,
                isLoaded: true
            };

        case CoursePathActions.ADD_COURSE_PATH:
            return {
                ...state,
                isLoaded: false
            };

        case CoursePathActions.ADD_COURSE_PATH_SUCCESS:
            return {
                ...state,
                coursePath: action.payload,
                isLoaded: true
            };

        case CoursePathActions.UPDATE_COURSE_PATH:
            return {
                ...state,
                isLoaded: false
            };

        case CoursePathActions.UPDATE_COURSE_PATH_SUCCESS:
            return {
                ...state,
                coursePath: action.payload,
                isLoaded: true
            };

        case CoursePathActions.DELETE_COURSE_PATH:
            return {
                ...state,
                isLoaded: false
            };

        case CoursePathActions.DELETE_COURSE_PATH_SUCCESS:
            return {
                ...state,
                isLoaded: true
            };

        case CoursePathActions.GET_COURE_PATH_LIST:
            return {
                ...state,
                isLoaded: false
            };

        case CoursePathActions.GET_COURSE_PATH_LIST_SUCCESS:
            return {
                ...state,
                paths: action.payload,
                isLoaded: true
            };

        case CoursePathActions.GET_USER_LIST:
            return {
                ...state,
                isLoaded: false
            };

        case CoursePathActions.GET_USER_LIST_SUCCESS:
            return {
                ...state,
                userPaths: action.payload,
                isLoaded: true
            };

        default:
            return state;
    }
}
