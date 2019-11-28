import * as CoursesActions from '../actions';
import { UserCourse } from '../models/user-course.model';
import { UserCourseDetails } from '../models/user-course-details.model';

export interface UserCoursesState{
    courses: UserCourse[];
    selectedCourse: UserCourseDetails,
    isLoaded: boolean;
}


const initialState: UserCoursesState = {
    courses: [],
    selectedCourse: null,
    isLoaded: false,
};

export function reducer(state: UserCoursesState = initialState, action: CoursesActions.Actions){
    switch(action.type){
        case CoursesActions.GET_COURSES_LIST:
            return {
                ...state,
                isLoaded: false
            };

        case CoursesActions.GET_COURSES_LIST_SUCCESS:

            return {
                ...state,
                courses: action.payload,
                selectedCourse: state.selectedCourse,
                isLoaded: true
            };

        case CoursesActions.GET_COURSE:
            return {
                ...state,
                isLoaded: false
            };

        case CoursesActions.GET_COURSE_SUCCESS:
            return {
                ...state,
                selectedCourse: action.payload,
                courses: state.courses,
                isLoading: false
            };

        case CoursesActions.START_COURSE:
            return {
                ...state,
                isLoaded: false
            };

        case CoursesActions.START_COURSE_SUCCESS:
            return {
                ...state,
                isLoaded: true
            };
        
        default:
            return state;
    }
}
