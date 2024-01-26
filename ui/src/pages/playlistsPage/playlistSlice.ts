import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import Playlist from '../../models/playlist';

interface PlaylistsState {
    playlists: Playlist[],
    totalPlaylists: number
}

const initialState: PlaylistsState = {
    playlists: [],
    totalPlaylists: 0
}

interface RequestParams {
    limit: number,
    offset: number
}

const playlistSlice = createSlice({
    name: 'playlist',
    initialState,
    reducers: {

    },
    extraReducers: (builder) => {
        builder
            .addCase(fetchPlaylists.pending, () => {
                console.log("fetchPlaylists.pending");
            })
            .addCase(fetchPlaylists.fulfilled, (state, action) => {
                state.playlists = state.playlists.concat(action.payload.data);
                state.totalPlaylists = action.payload.total;
            })
    }
});

export const fetchPlaylists = createAsyncThunk(
    "playlist/fetchPlaylists",
    async (params: RequestParams) => {
        const { limit, offset } = params;
        const response = await fetch(`https://localhost:7215/api/playlist?limit=${limit}&offset=${offset}`, 
        {
            credentials: 'include'
        });
        const playlists = await response.json();
        return playlists;
    });

export default playlistSlice.reducer;