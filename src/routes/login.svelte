<h1 style="margin-bottom: 1.5em">Login</h1>

<Button rel="external"
    href="/.auth/login/github?post_login_redirect_uri={baseUrl}/observations"
    icon={LogoGithub20}
    style="margin-right: 1.5em">
    Login with GitHub
</Button>
<Button rel="external"
    href="/.auth/login/twitter?post_login_redirect_uri={baseUrl}/observations"
    icon={LogoTwitter20}>
    Login with Twitter
</Button>

<script>
    import { Button } from 'carbon-components-svelte';
    import LogoGithub20 from 'carbon-icons-svelte/lib/LogoGithub20';
    import LogoTwitter20 from 'carbon-icons-svelte/lib/LogoTwitter20';

    let baseUrl = import.meta.env.VITE_PUBLIC_BASE_PATH;
</script>
