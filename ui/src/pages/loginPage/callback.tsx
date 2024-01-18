import { useEffect } from "react";
import { useSearchParams, useNavigate } from "react-router-dom"

const CallbackPage = () => {
    const navigate = useNavigate();
    const [searchParams] = useSearchParams();
    const code = searchParams.get('code');
    
    const getCallback = async () => {
        if (code) {
            const response = await fetch(`https://localhost:7215/api/auth/getCallback?request=${code}`,
            {
                credentials: 'include'
            });
            if (response && response.ok) {
                navigate('/playlists');
            }
        } 
        else {
            console.log('No code received');
        }
    }

    useEffect(() => {
        getCallback();
    }, [])

    return(
        <><div>hello</div></>
    );
}

export default CallbackPage;