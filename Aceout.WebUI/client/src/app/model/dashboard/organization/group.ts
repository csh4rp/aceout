import { User } from "../administration/user";

export class Group{
    id: number;
    name: string;
    userIds: number[];
    users: User[];
}