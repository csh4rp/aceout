import { UserLessonsEffects } from './user-lessons.effects';
import { fromEventPattern } from 'rxjs';

export const effects: any[] = [UserLessonsEffects];
export * from './user-lessons.effects';