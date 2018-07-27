#!/bin/bash
#工程绝对路径
project_path=[rp project_path /rp]
#工程名 将XXX替换成自己的工程名
project_name=[rp project_name /rp]
#plist文件所在路径
exportOptionsPath=[rp exportOptionsPath /rp]
#导出.ipa文件所在路径
exportIpaPath=[rp exportIpaPath /rp]
#build出的.app文件夹路径
build_path=[rp build_path /rp]
#重命名后的ipa文件名
package_name=[rp package_name /rp]
#打包模式 Debug/Release
development_mode=[rp isDebug /rp]
#scheme名 将XXX替换成自己的sheme名
scheme_name=Unity-iPhone

cd $project_path
pwd
echo '///-----------'
echo '/// 正在清理工程'
echo '///-----------'
xcodebuild -project ${project_path}/${scheme_name}.xcodeproj -target ${scheme_name} clean -quiet  || exit
echo '///--------'
echo '/// 清理完成'
echo '///--------'
echo ''
echo '///-----------'
echo '/// 正在编译工程:'
echo '///-----------'
xcodebuild archive -project ${project_path}/${scheme_name}.xcodeproj -scheme ${scheme_name} -configuration ${development_mode} -archivePath ${build_path}/${package_name}.xcarchive -quiet || exit
echo '///--------'
echo '/// 编译完成 开始ipa打包，请等待'
echo '///--------'
echo ''

xcodebuild -exportArchive -archivePath ${build_path}/${package_name}.xcarchive -configuration ${development_mode} -exportPath ${exportIpaPath} -exportOptionsPlist ${exportOptionsPath} -quiet || exit
if [ -e $exportIpaPath/$scheme_name.ipa ];
    then
    rm -rf $exportIpaPath/$package_name
    mv $exportIpaPath/$scheme_name.ipa $exportIpaPath/$package_name.ipa
    rm -rf $exportIpaPath/$scheme_name.ipa
    echo '///--------'
    echo '/// 打包成功！'
    echo '///--------'
else
    echo '///-----------'
    echo '/// failed!'
    echo '///-----------'
fi
open ${exportIpaPath}
exit 0