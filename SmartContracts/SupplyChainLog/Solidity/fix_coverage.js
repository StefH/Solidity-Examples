const replaceInFiles = require('replace-in-files');

const options = {
    files: 'contracts/*.html',

    from: /..\/..\/..\/..\/..\/..\/..\/..\/..\/../g,
    to: '../coverage',
};

replaceInFiles(options);
