function replaceEmotion(text) {

    // A regex alternation that looks for all of them (be careful to use escapes
    // where necessary)
    var searchFor = /:\)|:\(|;\)|:D|;\)|:-c|:-\*|:-h|:-SS|:v|^_^|B\|/gi;

    // A map mapping each smiley to its image
    var map = {
        ":)": 'happy.gif',
        ":(": 'sad.gif',
        ";)": 'winking.gif',
        ":D": 'big_grin.gif',

        ":-c": 'call_me.gif',
        ":-*": 'kiss.gif',
        ":-h": 'wave.gif',
        ":-SS": 'applause.gif',
        ":v": 'pacman.png',
        "^_^": 'kiki.png',
        "B|": 'sunglasses.png'
    };

    // Do the replacements
    text = text.replace(searchFor, function (match) {
        var rep;

        // Look up this match to see if we have an image for it
        rep = map[match];

        // If we do, return an `img` tag using that smiley icon; if not, there's
        // a mis-match between our `searchFor` regex and our map of
        // smilies, but handle it gracefully by returning the match unchanged.

        // The base URL of all our smilies
        var url = "/images/Emotions/";


        return rep ? "<img src='" + url + rep + "' />" : match;
    });

    return (text);
}