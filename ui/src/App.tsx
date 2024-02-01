import { BrowserRouter, Routes, Route } from 'react-router-dom'
import { ThemeProvider } from '@mui/material/styles'
import { createTheme } from "@mui/material/styles";
import { useMemo } from "react";
import PlaylistPage from './pages/playlistsPage'
import LoginPage from './pages/loginPage';
import CallbackPage from './pages/loginPage/callback';
import DuplicateTracksPage from './pages/duplicateTracksPage';

function App() {
  const theme = useMemo(() => createTheme(), []);

  return (
    <div className='app'>
      <BrowserRouter>
        {/* <ThemeProvider theme={theme}> */}
        <Routes>
          <Route
            path='/login'
            element={<LoginPage />} />
          <Route
            path='/callback'
            element={<CallbackPage />} />
          <Route
            path='/playlists'
            element={<PlaylistPage />} />
          <Route
            path='/duplicates'
            element={<DuplicateTracksPage />} />
        </Routes>
        {/* </ThemeProvider> */}
      </BrowserRouter>
    </div>
  )
}

export default App
