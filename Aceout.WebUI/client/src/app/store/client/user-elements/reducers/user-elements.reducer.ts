import { UserElement } from "../models/user-element.model";
import { UserElementAnswer } from "../models/user-element-answer.model";
import { ElementAnswer } from "../models/element-answer.model";
import * as UserElementActions from "../actions";


export interface UserElementsState {
    elements: { [id: number]: UserElement },
    selectedElement: UserElement,
    positions: number[],
    hasNext: boolean,
    hasPrevious: boolean,
    isSaved: boolean,
};

export const initialState: UserElementsState = {
    elements: null,
    selectedElement: null,
    positions: [],
    hasNext: false,
    hasPrevious: false,
    isSaved: false
};

export function reducer(state: UserElementsState = initialState, action: UserElementActions.Actions) {
    switch (action.type) {
        case UserElementActions.GET_ELEMENT_LIST:
            return {
                ...state
            };

        case UserElementActions.GET_ELEMENT_LIST_SUCCESS: {

            const entities = action.payload.reduce((entities: { [id: number]: UserElement }, element) => {
                return {
                    ...entities,
                    [element.elementId]: element
                };
            },
                {
                    ...state.elements
                }
            );

            const entityPositions = action.payload.sort((a, b) => {
                return a.position - b.position
            }).map(e => e.elementId);



            return {
                ...state,
                selectedElement: action.payload[0],
                elements: entities,
                positions: entityPositions,
            };
        }

        case UserElementActions.SAVE_ELEMENT:
            return {
                isSaved: false,
                ...state
            };


        case UserElementActions.SAVE_ELEMENT_SUCCESS: {
            const newState = {
                ...state,
                isSaved: true
            };
            newState.elements[action.payload.elementId].userAnswerModels = action.payload.answers;

            return newState;
        }

        case UserElementActions.NAVIGATE_NEXT:{

            const nextPosition = state.selectedElement.position + 1;
            const nextElementId = state.positions[nextPosition];
            const nextElement = state.elements[nextElementId];
            const hasNext = state.positions.length > nextPosition;

            return {
                ...state,
                selectedElement: nextElement,
                hasNext: hasNext,
                hasPrevious: true
            };

        }

        case UserElementActions.NAVIGATE_PREVIOUS: {

            const prevPosition = state.selectedElement.position - 1;
            const prevElementId = state.positions[prevPosition];
            const prevElement = state.elements[prevElementId];
            const hasPrev = prevPosition > 0;

            return {
                ...state,
                selectedElement: prevElement,
                hasNext: true,
                hasPrevious: hasPrev
            };
        }

        default:
            return {
                ...state
            };
    }
}
