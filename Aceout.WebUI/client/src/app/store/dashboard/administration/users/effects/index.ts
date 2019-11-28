import { UsersEffects } from './users.effects';
import { fromEventPattern } from 'rxjs';

export const effects: any[] = [UsersEffects];
export * from './users.effects';