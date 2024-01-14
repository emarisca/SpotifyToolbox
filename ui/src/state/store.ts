import { combineReducers, configureStore } from "@reduxjs/toolkit";
import playlistSlice from "../pages/playlistsPage/playlistSlice";

const reducer = combineReducers({
    playlistSlice
})

export const store = configureStore({
    reducer
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;